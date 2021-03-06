﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private Hints hints;
    private void Start()
    {
        if (DebugLog.Hints) Debug.Log("GameController sends requests to Hints");

        if (SceneSettings.Instance.currentScene == SceneSettings.SceneType.MainScene)
        {
            hints = GameObject.Find("Hint").GetComponent<Hints>();

            hints.Show(Hints.Entry.Walk);
            hints.Show(Hints.Entry.Letters);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    public static void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
