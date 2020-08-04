using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocaleController : MonoBehaviour
{
    public static void UpdateLocale(Locale.Language language)
    {
        Locale.language = language;

        UpdateLettersLocale();
        UpdateMenuLocale();
    }

    private static void UpdateMenuLocale()
    {
        FindObjectOfType<Menu>().SetText();
    }

    private static void UpdateLettersLocale()
    {
        Letter[] letters = FindObjectsOfType<Letter>();

        if (DebugLog.ChangeLocale)
            Debug.Log($"Updating locale in {letters.Length} letters");

        foreach (var letter in letters)
        {
            letter.SetText();
        }
    }
}
