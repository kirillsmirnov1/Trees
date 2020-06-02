using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private CameraController cameraController;

    void Start()
    {
        cameraController = GameObject.Find("Camera").GetComponent<CameraController>();    
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // Recalculate camera pos
        if(Input.GetKeyDown(KeyCode.C))
        {
            cameraController.MoveCameraToShowWholeTree();
        }
    }
}
