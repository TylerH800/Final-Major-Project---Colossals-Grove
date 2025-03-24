using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] GameInput GameInput;

    public Vector2 movementInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameInput = GetComponent<GameInput>();
    }

    private void OnMove(InputValue value)
    {
        
    }
}
