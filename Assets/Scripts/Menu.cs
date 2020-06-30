using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public static bool showingMenu = false;

    public void OnContinueClick()
    {
        gameObject.SetActive(false);
        PlayerInputHandler.LockCursor();
        showingMenu = false;
    }

    public void OnRestartClick()
    {
        GameController.RestartLevel();
        showingMenu = false;
    }

    public void OnExitClick()
    {
        Application.Quit();
    }
}
