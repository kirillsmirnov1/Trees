using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public float lookSensitivity = 1f;
    public float webglLookSensitivityMultiplier = 0.25f;

    private GUI gui;

    private static bool cursorLocked = true;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        gui = GameObject.Find("GUI").GetComponent<GUI>();
    }

    void Update()
    {
        if (cursorLocked && Input.GetKeyDown(KeyCode.Escape))
        {
            UnlockCursor();
            gui.ShowMenu();
        }

        if(!cursorLocked && Input.GetMouseButtonDown(0))
        {
            LockCursor();
            gui.HideMenu();
        }

        if(Input.GetMouseButtonDown(0) && Letter.ShowingAnyLetter)
        {
            gui.HideLetter();
        }
    }

    public static void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cursorLocked = true;
    }

    public static void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cursorLocked = false;
    }

    public Vector3 GetMoveInput()
    {
        if (CanProcessInput())
        {
            Vector3 move = new Vector3(Input.GetAxisRaw(GameConstants.k_AxisNameHorizontal), 0f, Input.GetAxisRaw(GameConstants.k_AxisNameVertical));
            move = Vector3.ClampMagnitude(move, 1);

            return move;
        }

        return Vector3.zero;
    }

    public float GetLookInputsHorizontal()
    {
        return GetLookAxis(GameConstants.k_MouseAxisNameHorizontal);
    }

    public float GetLookInputsVertical()
    {
        return GetLookAxis(GameConstants.k_MouseAxisNameVertical);
    }

    float GetLookAxis(string mouseInputName)
    {
        if (CanProcessInput())
        {
            float i = Input.GetAxisRaw(mouseInputName);

            if (mouseInputName == GameConstants.k_MouseAxisNameVertical)
            {
                i *= -1;
            }

            // apply sensitivity multiplier
            i *= lookSensitivity;

            // reduce mouse input amount to be equivalent to stick movement
            i *= 0.01f;
#if UNITY_WEBGL
            // Mouse tends to be even more sensitive in WebGL due to mouse acceleration, so reduce it even more
            i *= webglLookSensitivityMultiplier;
#endif

            return i;
        }

        return 0;
    }

    public bool CanProcessInput()
    {
        return Cursor.lockState == CursorLockMode.Locked && !Letter.ShowingAnyLetter;
    }
}
