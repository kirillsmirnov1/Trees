using TMPro;
using UnityEngine;

public class UiLetter : MonoBehaviour
{
    private string key;

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

    public void Show(string key, string text)
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
            case "08": Hints.Show(Hints.Entry.Destroying); break;
            // TODO case "": Hints.Show(Hints.Entry.Growing); break;
            case "final": Debug.Log("Game Over"); break;
        }
    }
}
