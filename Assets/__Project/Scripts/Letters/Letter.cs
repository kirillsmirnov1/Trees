using System;
using TMPro;
using UnityEngine;

public class Letter : MonoBehaviour, ITouchable
{
    [Tooltip("Unique letter key. Used for getting text from storage")]
    public LettersText.Entry key;

    /// <summary>
    /// Tells if any layer on scene is shown right now. Use it when checking if player can move
    /// </summary>
    public static bool ShowingAnyLetter { get => showingAnyLetter; }

    private TextMeshPro letterTextMesh;

    private static bool showingAnyLetter = false;

    private GUI gui;

    void Start()
    {
        letterTextMesh = transform.Find("Text").GetComponent<TextMeshPro>();
        gui = GameObject.Find("GUI").GetComponent<GUI>();


        SetText();
    }

    internal static void UiLetterIsShown()
    {
        showingAnyLetter = true;
    }

    internal void SetText()
    {
        letterTextMesh.text = LettersText.Get(key);
    }

    internal static void UiLetterIsHidden()
    {
        showingAnyLetter = false;
    }

    private void OnObjectClick()
    {
        if (DebugLog.LetterOpen)
            Debug.Log($"Letter.OnMouseDown()\nshowingAnyLetter: {showingAnyLetter}");

        if (!showingAnyLetter)
            gui.ShowLetter(key, letterTextMesh.text);

    }

    public void OnClick()
    {
        OnObjectClick();
    }
}
