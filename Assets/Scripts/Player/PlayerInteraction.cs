using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public PlayerInput playerInput;
    int inputIndex; //0 = m&k, 1 = controller
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        
    }

    private void Update()
    {
        CheckControlScheme();
    }

    void CheckControlScheme()
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

        print(inputIndex);
    }
}
