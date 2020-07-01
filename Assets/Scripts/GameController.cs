using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private void Start()
    {
        if (DebugLog.Hints) Debug.Log("GameController sends requests to Hints");
        Hints.Show(Hints.Entry.Walk);
        Hints.Show(Hints.Entry.Interaction);
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
