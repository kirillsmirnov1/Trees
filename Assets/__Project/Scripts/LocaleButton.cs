using UnityEngine;

public class LocaleButton : MonoBehaviour
{
    public Menu menu;
    public Locale.Language language;
    public void OnClick()
    {
        if(DebugLog.ChangeLocale) Debug.Log("LocaleButton.OnClick()");
        menu.LocaleButtonClick(language);
    }
}
