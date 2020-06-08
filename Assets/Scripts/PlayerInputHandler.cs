using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public float lookSensitivity = 1f;
    public float webglLookSensitivityMultiplier = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public Vector3 GetMoveInput()
    {
        Vector3 move = new Vector3(Input.GetAxisRaw(GameConstants.k_AxisNameHorizontal), 0f, Input.GetAxisRaw(GameConstants.k_AxisNameVertical));
        move = Vector3.ClampMagnitude(move, 1);

        return move;
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
        float i = Input.GetAxisRaw(mouseInputName);

        if(mouseInputName == GameConstants.k_MouseAxisNameVertical)
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
}
