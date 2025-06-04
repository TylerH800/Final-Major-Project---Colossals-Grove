using UnityEngine;
using UnityEngine.InputSystem;

public class CursorVisibilityManager : MonoBehaviour
{
    void OnInputActionChange(object obj, InputActionChange change)
    {
        if (change == InputActionChange.ActionPerformed)
        {
            InputAction inputAction = (InputAction)obj;
            InputControl lastControl = inputAction.activeControl;
            InputDevice lastDevice = lastControl.device;

            if (lastDevice.displayName == "Mouse")
            {
                Cursor.visible = true;
            }
            else
            {
                Cursor.visible = false;
            }
        }
    }

    void OnEnable()
    {
        InputSystem.onActionChange += OnInputActionChange;
    }

    void OnDisable()
    {
        InputSystem.onActionChange -= OnInputActionChange;
    }
}
