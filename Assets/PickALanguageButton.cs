using UnityEngine;
using UnityEngine.SceneManagement;

public class PickALanguageButton : MonoBehaviour
{
    public Locale.Language language;
    public void OnClick()
    {
        Locale.language = language;
        SceneManager.LoadScene("MainScene");
    }
}
