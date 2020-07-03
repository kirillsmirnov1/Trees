using Assets.Scripts.Tree;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static SceneSettings;

public class TreeController : MonoBehaviour, ITreeElementController
{
    [Header("References")]
    public TextMeshProUGUI branchesCounterText;
    public GameObject branchPrefab;

    [Header("Branches")]
    public int branchesPerTreeLimit = 1000;
    public int subBranchesPerBranchLimit = 3;
    public float newBranchMaxRotation = 70;
    public float subBranchScaleModificator = 0.9f;
    public float branchGeneratorRate = 0.25f;
    public float maxGrowthDelay = 0.5f;
    public float branchGrowthSpeed = 1f;

    [Header("Fog destroyer")]
    public float radiusPerFurthestBranch = 2f;
    public float radiusPerBranches = 0.04f;

    [Header("Debug")]
    public float showDebugRaysSeconds = 0.1f;

    public float LowestTreeY { get; private set; } = 0;
    public float HighestTreeY { get; private set; } = 0;

    TreeController ITreeElementController.TreeController => this;

    private CameraController cameraController;
    private SceneSettings SceneSettings => sceneSettings.Value;
    private readonly Lazy<SceneSettings> sceneSettings 
        = new Lazy<SceneSettings>(() => GameObject.Find("SceneSettings").GetComponent<SceneSettings>());
    private SphereCollider FogDestroyer => fogDestroyer.Value;
    private Lazy<SphereCollider> fogDestroyer;

    private int numberOfBranches = 0;
    private float furthestBranch = 0f;
    private readonly float branchRadius = 0.3f;

    private readonly List<BranchController> branches = new List<BranchController>();
    private Coroutine autoGrowthCoroutine;

    void Awake()
    {
        fogDestroyer = new Lazy<SphereCollider>(() => transform.Find("FogDestroyer").GetComponent<SphereCollider>());

        if (SceneSettings.currentScene == SceneType.FloatingTree)
        {
            cameraController = GameObject.Find("Camera").GetComponent<CameraController>();
        }
    }

    void Update()
    {
        if (SceneSettings.currentScene == SceneType.FloatingTree || SceneSettings.currentScene == SceneType.TreeOnACliff)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                GenerateNewBranches();
            }
        }
    }

    public void ResetTree()
    {
        if (autoGrowthCoroutine != null)
            StopCoroutine(autoGrowthCoroutine);

        numberOfBranches = 0;
        furthestBranch = 0;

        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out BranchController branch))
                branch.DestroyBranch();
        }

        branches.Clear();
        ResizeFogDestroyer();
    }
    public void GrowFullTree()
    {
        autoGrowthCoroutine = StartCoroutine(GenerateAllBranches());
    }

    private IEnumerator GenerateAllBranches()
    {
        while (branches.Count < branchesPerTreeLimit)
        {
            GenerateNewBranches();
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void GenerateNewBranches()
    {
        if (branches.Count == 0)
        {
            GenerateFirstBranch();
        }
        else if (branches.Count < branchesPerTreeLimit) // TODO handle branches.Count >= 1000
        {
            List<BranchController> newBranches = new List<BranchController>();

            int branchesToGenerate = (int)System.Math.Ceiling(numberOfBranches * branchGeneratorRate);
            //Debug.Log("branchesToGenerate: " + branchesToGenerate);

            while (branchesToGenerate-- > 0)
            {
                int randomPos = UnityEngine.Random.Range(0, branches.Count);
                BranchController randomBranch = branches[randomPos];

                if (randomBranch.CanGrowSubBranches())
                {
                    GenerateBranch(randomBranch, out BranchController newBranch, out bool canBranchGenerateMore);

                    if (!canBranchGenerateMore)
                    {
                        branches.RemoveAt(randomPos);
                    }

                    if (newBranch != null)
                    {
                        newBranches.Add(newBranch);
                    }
                }
            }

            //Debug.Log($"Generated branches: {newBranches.Count}");
            //Debug.Log($"Fog destroyer radius: {fogDestroyer.radius}");
            //Debug.Log($"Total branches: {branches.Count}");

            branches.AddRange(newBranches);
        }
    }

    private void GenerateBranch(BranchController parentBranch, out BranchController newBranch, out bool canBranchGenerateMore)
    {
        parentBranch.subBranches++;
        int newBranchGeneration = parentBranch.generation + 1;

        Transform head = parentBranch.transform.Find("Head");
        Transform tail = parentBranch.transform.Find("Tail");

        Quaternion rotation = Quaternion.Euler(
            parentBranch.transform.rotation.eulerAngles
            + Vector3.right * UnityEngine.Random.Range(-newBranchMaxRotation, newBranchMaxRotation) // TODO [-1 or 1] * Random(30, 70)
            + Vector3.forward * UnityEngine.Random.Range(-newBranchMaxRotation, newBranchMaxRotation)
            );

        if (NoIntersections(head, tail, rotation * Vector3.up, newBranchGeneration))
        {
            GameObject subBranch = Instantiate(branchPrefab, head.position, rotation);
            subBranch.transform.parent = parentBranch.transform;

            newBranch = subBranch.GetComponent<BranchController>();

            newBranch.generation = newBranchGeneration;
        }
        else
        {
            newBranch = null;
        }

        canBranchGenerateMore = parentBranch.subBranches < subBranchesPerBranchLimit;
    }

    private bool NoIntersections(Transform head, Transform tail, Vector3 direction, int newBranchGeneration)
    {
        float maxDistance = (head.position - tail.position)
            .magnitude / subBranchScaleModificator;

        float radius = branchRadius * Mathf.Pow(subBranchScaleModificator, newBranchGeneration);

        bool rayHitSmth = Physics.CapsuleCast(head.position, head.position, radius, direction, maxDistance);

        Debug.DrawRay(head.position, direction * maxDistance,
            rayHitSmth ? Color.red : Color.green,
            showDebugRaysSeconds, false);

        return !rayHitSmth;
    }

    private void GenerateFirstBranch()
    {
        BranchController branch = Instantiate(branchPrefab, transform).GetComponent<BranchController>();
        branches.Add(branch);
    }

    public void CheckNewBranch(Vector3 pos)
    {
        numberOfBranches++;
        ResizeFogDestroyer();

        float branchDistanceFromRoot = Vector3.Distance(transform.position, pos);
        if (branchDistanceFromRoot > furthestBranch)
        {
            //Debug.Log($"branchDistanceFromRoot: {branchDistanceFromRoot}");
            furthestBranch = branchDistanceFromRoot;
        }

        if (SceneSettings.currentScene == SceneType.FloatingTree)
        {
            UpdateBranchesText();

            bool centerMoved = false;

            if (pos.y < LowestTreeY)
            {
                LowestTreeY = pos.y;
                centerMoved = true;
            }
            else if (pos.y > HighestTreeY)
            {
                HighestTreeY = pos.y;
                centerMoved = true;
            }

            if (centerMoved)
            {
                cameraController.ShowWholeTree((HighestTreeY + LowestTreeY) / 2f, HighestTreeY - LowestTreeY);
            }
        }
    }

    private void ResizeFogDestroyer()
    {
        FogDestroyer.radius = (furthestBranch * radiusPerFurthestBranch + radiusPerBranches * numberOfBranches) * 0.5f;
    }

    private void UpdateBranchesText()
    {
        if (branchesCounterText != null && SceneSettings.currentScene == SceneType.FloatingTree)
        {
            branchesCounterText.text = "Branches: " + numberOfBranches;
        }
    }
}
