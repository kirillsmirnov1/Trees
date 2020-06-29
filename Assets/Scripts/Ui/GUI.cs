using System.Collections;
using TMPro;
using UnityEngine;

public class GUI : MonoBehaviour
{
    public UiLetter uiLetter;

    public void ShowLetter(string text)
    {
        uiLetter.gameObject.SetActive(true);
        uiLetter.SetText(text);
        StartCoroutine(DelayMessageAboutLetterBeingShown());
    }

    public void HideLetter()
    {
        uiLetter.gameObject.SetActive(false);
        Letter.UiLetterIsHidden();
    }

    private IEnumerator DelayMessageAboutLetterBeingShown()
    {
        yield return null;
        Letter.UiLetterIsShown();
    }
}
