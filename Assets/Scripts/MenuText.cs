using System.Collections.Generic;

public class MenuText
{
    public enum Entry { Continue, Restart, Exit }
    private static readonly Dictionary<Locale.Language, Dictionary<Entry, string>> text
        = new Dictionary<Locale.Language, Dictionary<Entry, string>>
        {
            [Locale.Language.Ru] = new Dictionary<Entry, string>
            {
                [Entry.Continue] = "Продолжить",
                [Entry.Restart] = "Перезапуск",
                [Entry.Exit] = "Выйти"
            },

            [Locale.Language.En] = new Dictionary<Entry, string>
            {
                [Entry.Continue] = "Continue",
                [Entry.Restart] = "Reset",
                [Entry.Exit] = "Exit"
            }
        };

    public static string Get(Entry entry) => text[Locale.language][entry];
}
