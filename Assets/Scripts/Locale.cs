using UnityEngine;

public class Locale : MonoBehaviour
{
    public enum Language { Ru, En }
    public static Language language;

    public static void UpdateLocale(Language language)
    {
        Locale.language = language;

        UpdateLettersLocale();
        // TODO hints
        // TODO menu
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
