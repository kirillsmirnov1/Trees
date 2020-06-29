using TMPro;
using UnityEngine;

public class UiLetter : MonoBehaviour
{
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

    public void SetText(string text)
    {
        LetterTextMesh.text = text;
    }
}
