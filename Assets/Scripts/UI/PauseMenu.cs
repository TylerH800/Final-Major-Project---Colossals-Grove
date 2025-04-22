using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject menu;
    
    public void ToggleMenu()
    {
        if (menu.activeSelf)
        {
            menu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            menu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
