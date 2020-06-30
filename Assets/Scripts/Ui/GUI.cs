using System.Collections;
using TMPro;
using UnityEngine;

public class GUI : MonoBehaviour
{
    public UiLetter uiLetter; // TODO find from code
    private GameObject menu;

    private void Start()
    {
        menu = transform.Find("Menu").gameObject;
    }

    public void ShowMenu() => menu.SetActive(true);
    public void HideMenu() => menu.SetActive(false);

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
