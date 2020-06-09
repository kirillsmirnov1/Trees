using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static SceneSettings;

public class TreeController : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI branchesCounterText;
    public GameObject branchPrefab;

    [Header("Branches")]
    public float newBranchMaxRotation = 70;
    public int subBranchesPerBranchLimit;
    public float subBranchScaleModificator;
    public float branchGeneratorRate = 0.25f;

    [Header("Tree aura")]
    public float radiusPerBranches = 0.04f;

    [Header("Debug")]
    public float showDebugRaysSeconds = 0.1f;

    public float lowestTreeY { get; private set; } = 0;
    public float highestTreeY { get; private set; } = 0;

    private CameraController cameraController;
    private SceneSettings sceneSettings;
    private SphereCollider fogDestroyer;

    private int numberOfBranches = 0;

    private List<BranchController> branches = new List<BranchController>();

    // Start is called before the first frame update
    void Start()
    {
        sceneSettings = GameObject.Find("SceneSettings").GetComponent<SceneSettings>();
        fogDestroyer = transform.Find("FogDestroyer").GetComponent<SphereCollider>();

        if (sceneSettings.currentScene == SceneType.FloatingTree)
        {
            cameraController = GameObject.Find("Camera").GetComponent<CameraController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            GenerateNewBranches();
        }
    }

    public void GenerateNewBranches()
    {
        if(branches.Count == 0)
        {
            GenerateFirstBranch();
        }
        else if (branches.Count < 1000) // TODO handle branches.Count >= 1000
        {
            List<BranchController> newBranches = new List<BranchController>();

            int branchesToGenerate = (int)System.Math.Ceiling(numberOfBranches * branchGeneratorRate);
            Debug.Log("branchesToGenerate: " + branchesToGenerate);

            while(branchesToGenerate-- > 0)
            {
                int randomPos = Random.Range(0, branches.Count);

                GenerateBranch(branches[randomPos], out BranchController newBranch, out bool canBranchGenerateMore);

                if (!canBranchGenerateMore)
                {
                    branches.RemoveAt(randomPos);
                }

                if (newBranch != null)
                {
                    newBranches.Add(newBranch);
                }
            }

            Debug.Log($"Generated branches: {newBranches.Count}");
            Debug.Log($"Total branches: {branches.Count}");

            fogDestroyer.radius = branches.Count * radiusPerBranches;
            Debug.Log($"Fog destroyer radius: {fogDestroyer.radius}");

            branches.AddRange(newBranches);
        }
    }

    private void GenerateBranch(BranchController parentBranch, out BranchController newBranch, out bool canBranchGenerateMore)
    {
        parentBranch.subBranches++;

        Transform head = parentBranch.transform.Find("Head");
        Transform tail = parentBranch.transform.Find("Tail");

        Quaternion rotation = Quaternion.Euler(
            parentBranch.transform.rotation.eulerAngles
            + Vector3.right * Random.Range(-newBranchMaxRotation, newBranchMaxRotation) // TODO [-1 or 1] * Random(30, 70)
            + Vector3.forward * Random.Range(-newBranchMaxRotation, newBranchMaxRotation)
            );

        if (NoIntersections(head, tail, rotation * Vector3.up))
        {
            GameObject subBranch = Instantiate(branchPrefab, head.position, rotation);
            subBranch.transform.parent = parentBranch.transform;
            subBranch.transform.localScale = subBranchScaleModificator * Vector3.one;

            newBranch = subBranch.GetComponent<BranchController>();
        }
        else
        {
            newBranch = null;
        }

        canBranchGenerateMore = parentBranch.subBranches < subBranchesPerBranchLimit;
    }

    private bool NoIntersections(Transform head, Transform tail, Vector3 direction)
    {
        Ray ray = new Ray(head.position, direction);

        float maxDistance = (head.position - tail.position)
            .magnitude / subBranchScaleModificator;

        bool rayHitSmth = Physics.Raycast(ray, maxDistance);

        Debug.DrawRay(head.position, direction * maxDistance,
            rayHitSmth ? Color.red : Color.green,
            showDebugRaysSeconds, false);

        return !rayHitSmth;
    }

    private void GenerateFirstBranch()
    {
        branches.Add(Instantiate(branchPrefab, transform).GetComponent<BranchController>());
    }

    public void CheckNewBranch(float y)
    {
        numberOfBranches++;

        if (sceneSettings.currentScene == SceneType.FloatingTree)
        {
            UpdateBranchesText();

            bool centerMoved = false;

            if (y < lowestTreeY)
            {
                lowestTreeY = y;
                centerMoved = true;
            }
            else if (y > highestTreeY)
            {
                highestTreeY = y;
                centerMoved = true;
            }

            if (centerMoved)
            {
                cameraController.ShowWholeTree((highestTreeY + lowestTreeY) / 2f, highestTreeY - lowestTreeY);
            }
        }
    }

    private void UpdateBranchesText()
    {
        if (branchesCounterText != null && sceneSettings.currentScene == SceneType.FloatingTree)
        {
            branchesCounterText.text = "Branches: " + numberOfBranches;
        }
    }
}
