using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hints : MonoBehaviour
{
    public enum Entry
    {
        Walk,
        Interaction,
        Growing,
        Destroying,
        End
    }

    private static readonly Dictionary<Locale.Language, Dictionary<Entry, string>> Text =
        new Dictionary<Locale.Language, Dictionary<Entry, string>>
        {
            [Locale.Language.Ru] = new Dictionary<Entry, string>
            {
                [Entry.Walk] = "Нажимай WASD чтобы ходить",
                [Entry.Interaction] = "Нажимай на письма и сферы левой кнопкой мыши",
                [Entry.Growing] = "E + левая кнопка мыши = быстрый рост дерева",
                [Entry.Destroying] = "Q + ЛКМ = уничтожение дерева",
                [Entry.End] = "Спасибо за игру"
            },

            [Locale.Language.En] = new Dictionary<Entry, string>
            {
                [Entry.Walk] = "Press WASD to walk",
                [Entry.Interaction] = "Click letters and spheres with left mouse button",
                [Entry.Growing] = "E + left mouse button = fast tree growth",
                [Entry.Destroying] = "Q + LMB destroys tree",
                [Entry.End] = "Thanks for playing!"
            }
        };

    private Animator animator;
    private TextMeshProUGUI text;

    private readonly Queue<Entry> hintsToShow = new Queue<Entry>();
    private Coroutine animationCoroutine = null;

    private void Awake()
    {
        if (DebugLog.Hints) Debug.Log("Hints.Awake()");
        animator = GetComponent<Animator>();
        text = GetComponent<TextMeshProUGUI>();
    }

    public void Show(Entry entry)
    {
        if (DebugLog.Hints) Debug.Log($"Hints.Show({entry})");
        hintsToShow.Enqueue(entry);

        if (animationCoroutine == null)
            animationCoroutine = StartCoroutine(ActuallyShowHint(hintsToShow.Dequeue()));
    }

    private IEnumerator ActuallyShowHint(Entry entry)
    {
        if (DebugLog.Hints) Debug.Log($"Hints.ActuallyShowHint({entry})");

        text.text = Text[Locale.language][entry];

        animator.SetBool("showing", true);

        yield return new WaitForSeconds(3);

        animator.SetBool("showing", false);

        yield return new WaitForSeconds(1);

        if (hintsToShow.Count > 0)
            animationCoroutine = StartCoroutine(ActuallyShowHint(hintsToShow.Dequeue()));
        else
            animationCoroutine = null;
    }
}
