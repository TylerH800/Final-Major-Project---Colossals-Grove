using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public PlayerInput playerInput;
    private GameInput gameInput;
    int inputIndex; //0 = m&k, 1 = controller
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();   
        gameInput = new GameInput();
    }

    private void Update()
    {
        CheckControlScheme();
    }

    public void CheckControlScheme()
    {
        if (playerInput.currentControlScheme == null)
        {
            return;
        }

        if (playerInput.currentControlScheme == "Keyboard and Mouse")
        {
            inputIndex = 0;
        }
        else
        {
            inputIndex = 1;
        }
    }

    public void ToggleEliState(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EventManager.OnChangeEliAIMode();
            print("Eli toggle");
        }
    }

    public void ToggleLedaState(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EventManager.OnChangeLedaAIMode();
            print("Leda toggle");
        }        
    }
}
