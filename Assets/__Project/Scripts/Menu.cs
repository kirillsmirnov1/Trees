using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public TextMeshProUGUI ContinueButtonText;
    public TextMeshProUGUI RestartButtonText;
    public TextMeshProUGUI ExitButtonText;

    public Button RuLocaleButton;
    public Button EnLocaleButton;

    public static bool showingMenu = false;

    private void Awake()
    {
        SetText();
    }

    public void OnContinueClick()
    {
        gameObject.SetActive(false);
        PlayerInputHandler.LockCursor();
        showingMenu = false;
    }

    internal void SetText()
    {
        ContinueButtonText.text = MenuText.Get(MenuText.Entry.Continue);
        RestartButtonText.text = MenuText.Get(MenuText.Entry.Restart);
        ExitButtonText.text = MenuText.Get(MenuText.Entry.Exit);
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
        LocaleController.UpdateLocale(language);
    }
}
