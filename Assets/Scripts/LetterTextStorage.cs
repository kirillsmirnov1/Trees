using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterTextStorage : MonoBehaviour
{
    public static Dictionary<string, string> Text = new Dictionary<string, string> 
    {
        ["01"] = "Oh well, that is first letter indeed",
        ["02"] = "You found the second one, that a good seeker you are!"
    };
}
