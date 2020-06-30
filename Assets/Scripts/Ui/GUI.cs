using System.Collections;
using TMPro;
using UnityEngine;

public class GUI : MonoBehaviour
{
    private UiLetter uiLetter;
    private GameObject menu;

    private void Start()
    {
        menu = transform.Find("Menu").gameObject;
        uiLetter = transform.Find("UiLetter").GetComponent<UiLetter>();
    }

    public void ShowMenu()
    {
        Menu.showingMenu = true;
        menu.SetActive(true);
    }
    public void HideMenu()
    {
        menu.SetActive(false);
        Menu.showingMenu = false;
    }

    public void ShowLetter(string key, string text)
    {
        uiLetter.Show(key, text);
        StartCoroutine(DelayMessageAboutLetterBeingShown());
    }

    public void HideLetter()
    {
        uiLetter.Hide();
        Letter.UiLetterIsHidden();
    }

    private IEnumerator DelayMessageAboutLetterBeingShown()
    {
        yield return null;
        Letter.UiLetterIsShown();
    }
}
