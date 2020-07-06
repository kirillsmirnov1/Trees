using System;
using TMPro;
using UnityEngine;

public class UiLetter : MonoBehaviour
{
    private LettersText.Entry key;

    private TextMeshProUGUI LetterTextMesh => _letterTextMesh;
    private TextMeshProUGUI _letterTextMesh;

    private Hints HintsRef => _hints.Value;
    private readonly Lazy<Hints> _hints = new Lazy<Hints>(() => GameObject.Find("Hint").GetComponent<Hints>());

    private void Awake()
    {
        _letterTextMesh = transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }

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
            case LettersText.Entry.L01: HintsRef.Show(Hints.Entry.Grow); break;
            case LettersText.Entry.L04: HintsRef.Show(Hints.Entry.FastGrow); break;
            case LettersText.Entry.L08: HintsRef.Show(Hints.Entry.Destroying); break;
            case LettersText.Entry.Final: HintsRef.Show(Hints.Entry.End); break;
        }
    }
}
