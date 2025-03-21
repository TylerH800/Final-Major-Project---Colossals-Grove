using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void ExitGame()
    {
        print("Exit Game");
        PlayerPrefs.Save();
        Application.Quit();
    }
}
