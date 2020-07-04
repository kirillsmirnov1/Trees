using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button RuLocaleButton;
    public Button EnLocaleButton;

    public static bool showingMenu = false;

    public void OnContinueClick()
    {
        gameObject.SetActive(false);
        PlayerInputHandler.LockCursor();
        showingMenu = false;
    }

    public void OnRestartClick()
    {
        GameController.RestartLevel();
        showingMenu = false;
    }

    public void OnExitClick()
    {
        Application.Quit();
    }

    public void LocaleButtonClick(Locale.Language language)
    {
        Locale.UpdateLocale(language);
    }
}
