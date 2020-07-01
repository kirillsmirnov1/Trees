﻿using System;
using TMPro;
using UnityEngine;

public class Letter : MonoBehaviour
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

        letterTextMesh.text = LettersText.Get(key);

    }

    private void OnMouseDown()
    {
        if (DebugLog.LetterOpen)
            Debug.Log($"Letter.OnMouseDown()\nshowingAnyLetter: {showingAnyLetter}");

        if(!showingAnyLetter)
            gui.ShowLetter(key, letterTextMesh.text);
    }

    internal static void UiLetterIsShown()
    {
        showingAnyLetter = true;
    }

    internal static void UiLetterIsHidden()
    {
        showingAnyLetter = false;
    }
}
