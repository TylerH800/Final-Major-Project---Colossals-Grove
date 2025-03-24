using System.Collections;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public SoundObject titleMusic;
    private void Start()
    {
        InvokeRepeating("TitleMusic", 0.5f, titleMusic.soundClip.length);
    }
    public void StartGameButton()
    {

    }
    public void ExitGame()
    {
        print("Exit Game");
        PlayerPrefs.Save();
        Application.Quit();
    }

    void TitleMusic()
    {
        AudioManager.Instance.PlayMusic(titleMusic);
    }
}
