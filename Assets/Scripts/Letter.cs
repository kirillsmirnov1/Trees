using TMPro;
using UnityEngine;

public class Letter : MonoBehaviour
{
    public string letterText = "Hi there!";

    [Tooltip("Material used when letter is resting")]
    public Material restMaterial;
    [Tooltip("Material used when letter is shown to player. This material should have emission with the same texture as in Albedo")]
    public Material showMaterial;

    /// <summary>
    /// Tells if any layer on scene is shown right now. Use it when checking if player can move
    /// </summary>
    public static bool ShowingAnyLetter { get => showingAnyLetter; }

    private TextMeshPro letterTextMesh;

    private Vector3 restPosition;
    private Quaternion restRotation;

    private readonly static Vector3 restScale = new Vector3(0.5f, 0.5f, 1e-5f);
    private readonly static Vector3 showScale = new Vector3(0.4f, 0.4f, 1e-5f);

    private bool showingLetter = false;
    private static bool showingAnyLetter = false;

    private const string LetterPlaceholderObjectName = "LetterPlaceholder";
    private const string IgnoreLightLayerName = "IgnoreLight";

    void Start()
    {
        CheckPrerequisites();

        letterTextMesh = transform.Find("Text").GetComponent<TextMeshPro>();
        restPosition = transform.position;
        restRotation = transform.rotation;
    }

    private void CheckPrerequisites()
    {
        if(Camera.main.transform.Find(LetterPlaceholderObjectName) == null)
        { 
            Debug.LogError("Couldn't find placeholder for letter on main camera"); 
        }

        if(LayerMask.NameToLayer(IgnoreLightLayerName) == -1)
        {
            Debug.LogError("Couldn't find IgnoreLight layer");
        }
    }

    void Update()
    {
        if (letterTextMesh.text != letterText)
        {
            letterTextMesh.text = letterText;
        }
    }

    private void OnMouseDown()
    {
        HandleClickOnLetter();
    }

    private void HandleClickOnLetter()
    {
        if (showingLetter)
        {
            MoveToRest();
        }
        else if (!showingAnyLetter) // Don't show new letter, if some letter is already shown
        {
            ShowToPlayer();
        }
    }

    private void MoveToRest()
    {
        showingLetter = false;
        showingAnyLetter = false;

        transform.position = restPosition;
        transform.localScale = restScale;
        transform.rotation = restRotation;

        GetComponent<MeshRenderer>().material = restMaterial;
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    private void ShowToPlayer()
    {
        Transform LetterPlaceholder = Camera.main.transform.Find(LetterPlaceholderObjectName);

        showingLetter = true;
        showingAnyLetter = true;

        transform.position = LetterPlaceholder.position;
        transform.localScale = showScale;
        transform.rotation = LetterPlaceholder.rotation;

        GetComponent<MeshRenderer>().material = showMaterial;
        gameObject.layer = LayerMask.NameToLayer(IgnoreLightLayerName);
    }
}
