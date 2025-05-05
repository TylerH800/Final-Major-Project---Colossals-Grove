using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{    
    private GameInput gameInput;

    //prompt
    public GameObject prompt;
    public TextMeshProUGUI promptText;

    private Transform cam;
    public LayerMask whatIsObstacle;

    int inputIndex; //0 = m&k, 1 = controller
    private void Start()
    {       
        gameInput = new GameInput();
        cam = Camera.main.gameObject.transform;
    }

    private void Update()
    { 
        CheckForInteraction();
        
    }    

    public void ToggleEliState(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EventManager.OnChangeEliAIMode();           
        }
    }

    public void ToggleLedaState(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EventManager.OnChangeLedaAIMode();          
        }        
    }

    void CheckForInteraction()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100, whatIsObstacle))
        {           
            GameObject parent = hit.transform.root.gameObject
                ;
          
            if (parent.layer != LayerMask.NameToLayer("Obstacle"))
            {
                prompt.SetActive(false);
                return;
            }

            prompt.SetActive(true);

            switch (parent.tag)
            {
                case "Gate":
                    promptText.text = "Open Gate (Eli)";
                    break;
                case "Barrel":
                    promptText.text = "Ignite (Eli)";
                    break;
                case "NeutralColossal":
                    promptText.text = "Bait an attack (Leda)";
                    break;
                default:
                    prompt.SetActive(false);
                    break;

                  
            }
        }
        else
        {           
            prompt.SetActive(false);
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (!context.performed) 
        {
            return;
        }
    
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, 100, whatIsObstacle))
        {
            GameObject parent = hit.transform.root.gameObject;

            if (parent.layer != LayerMask.NameToLayer("Obstacle"))
            {
                return;
            }

            /*
            if (hit.transform.CompareTag("Gate"))
            {
                EventManager.OnEliGate(hit.point);
                parent.layer = LayerMask.NameToLayer("Default");
            }

            if (hit.transform.CompareTag("Barrel"))
            {                
                EventManager.OnEliIgnite(hit.transform.root.position);
                parent.layer = LayerMask.NameToLayer("Default");
            }

            if (hit.transform.CompareTag("NeutralColossal"))
            {
                EventManager.OnLedaBait(hit.transform.root.position);
                parent.layer = LayerMask.NameToLayer("Default");
            } */

            switch (parent.tag)
            {
                case "Gate":
                    EventManager.OnEliGate(hit.point);
                    parent.layer = LayerMask.NameToLayer("InactiveObstacle");
                    break;
                case "Barrel":
                    EventManager.OnEliIgnite(parent.transform.position);
                    
                    break;
                case "NeutralColossal":
                    EventManager.OnLedaBait(parent.transform.position);
                    parent.layer = LayerMask.NameToLayer("InactiveObstacle");
                    break;          
            }
        }
    }

    
}
