using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject menu;
    public static bool gamePaused;
    public PlayerInput playerInput;

    public SoundObject buttonClick;

    private bool canSwitch = true; //input bleeding on first frame
   
    public void Pause()
    {
        if (!gamePaused && canSwitch)
        {
            StartCoroutine(InputCooldown());
            playerInput.SwitchCurrentActionMap("UI");
            menu.SetActive(true);
            Time.timeScale = 0f;
            gamePaused = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }        
    }

    public void Unpause()
    {
        if (gamePaused &&canSwitch)
        {
            StartCoroutine(InputCooldown());
            playerInput.SwitchCurrentActionMap("Player");
            menu.SetActive(false);
            Time.timeScale = 1f;
            gamePaused = false;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }        
    }

    IEnumerator InputCooldown()
    {
        canSwitch = false;
        yield return new WaitForSecondsRealtime(0.1f);
        canSwitch = true;
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene("Frontend");
    }

    public void QuitGame()
    {
        EventManager.OnQuitGame();
    }

    public void ButtonClickSound()
    {
        AudioManager.Instance.PlaySFX(buttonClick);
    }
}
