using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hints : MonoBehaviour
{
    public enum Entry
    {
        Walk,
        Letters,
        Grow,
        FastGrow,
        Destroying,
        End
    }

    private static readonly Dictionary<Locale.Language, Dictionary<Entry, string>> Text =
        new Dictionary<Locale.Language, Dictionary<Entry, string>>
        {
            [Locale.Language.Ru] = new Dictionary<Entry, string>
            {
                [Entry.Walk] = "Нажимай WASD чтобы ходить",
                [Entry.Letters] = "Нажимай на письма левой кнопкой мыши",
                [Entry.Grow] = "Нажимай на корневище и ветки дерева, чтобы оно росло",
                [Entry.FastGrow] = "E + клик = быстрый рост дерева",
                [Entry.Destroying] = "Q + клик = уничтожение дерева",
                [Entry.End] = "Спасибо за игру"
            },

            [Locale.Language.En] = new Dictionary<Entry, string>
            {
                [Entry.Walk] = "Press WASD to walk",
                [Entry.Letters] = "Click letters and spheres with left mouse button",
                [Entry.Grow] = "Click on root and branches of tree for it to grow",
                [Entry.FastGrow] = "E + click = fast tree growth",
                [Entry.Destroying] = "Q + click destroys tree",
                [Entry.End] = "Thanks for playing!"
            }
        };

    private Animator animator;
    private TextMeshProUGUI text;

    private readonly Queue<Entry> hintsToShow = new Queue<Entry>();
    private readonly HashSet<Entry> shownHints = new HashSet<Entry>();
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

        if (shownHints.Add(entry)) //Show every hint only once
        {
            hintsToShow.Enqueue(entry);

            if (animationCoroutine == null)
                animationCoroutine = StartCoroutine(ActuallyShowHint(hintsToShow.Dequeue()));
        }
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
