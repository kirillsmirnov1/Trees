using TMPro;
using UnityEngine;

public class UiLetter : MonoBehaviour
{
    private LettersText.Entry key;

    private TextMeshProUGUI LetterTextMesh
    {
        get
        {
            if(letterTextMesh == null)
            {
                letterTextMesh = transform.Find("Text").GetComponent<TextMeshProUGUI>();
            }

            return letterTextMesh;
        }
    }
    private TextMeshProUGUI letterTextMesh;

    public void Show(LettersText.Entry key, string text)
    {
        gameObject.SetActive(true);
        LetterTextMesh.text = text;
        this.key = key;
    }

    public void Hide()
    {
        gameObject.SetActive(false);

        switch (key)
        {
            case LettersText.Entry.L08: Hints.Show(Hints.Entry.Destroying); break;
            case LettersText.Entry.L03: Hints.Show(Hints.Entry.Growing); break;
            case LettersText.Entry.Final: Debug.Log("Game Over"); break;
        }
    }
}
