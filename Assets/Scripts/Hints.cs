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
        Destroying
    }

    private static readonly Dictionary<string, string> Text = new Dictionary<string, string>
    {
        [Entry.Walk.ToString()]         = "Нажимай WASD чтобы ходить",
        [Entry.Interaction.ToString()]  = "Нажимай на письма и сферы левой кнопкой мыши",
        [Entry.Growing.ToString()]      = "E + левая кнопка мыши = быстрый рост дерева",
        [Entry.Destroying.ToString()]   = "Q + ЛКМ = уничтожение дерева"
    };

    private Animator animator;
    private TextMeshProUGUI text;

    private static readonly Queue<Entry> hintsToShow = new Queue<Entry>();
    private Coroutine animationCoroutine = null;

    private void Start()
    {
        if (DebugLog.Hints) Debug.Log("Hints.Start()");
        animator = GetComponent<Animator>();
        text = GetComponent<TextMeshProUGUI>();
    }

    public static void Show(Entry entry)
    {
        if (DebugLog.Hints) Debug.Log($"Hints.Show({entry})");
        hintsToShow.Enqueue(entry);
    }

    private void Update()
    {
        if(hintsToShow.Count > 0 && animationCoroutine == null)
        {
            if (DebugLog.Hints) Debug.Log("Hints.Update() if called");
            animationCoroutine = StartCoroutine(ActuallyShowHint(hintsToShow.Dequeue()));
        }
    }

    private IEnumerator ActuallyShowHint(Entry entry)
    {
        if (DebugLog.Hints) Debug.Log($"Hints.ActuallyShowHint({entry})");

        text.text = Text[entry.ToString()];

        animator.SetBool("showing", true);

        yield return new WaitForSeconds(3);

        animator.SetBool("showing", false);

        yield return new WaitForSeconds(1);

        animationCoroutine = null;
    }
}
