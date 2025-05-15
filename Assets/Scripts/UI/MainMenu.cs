using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject loadingInterface;
    public Image loadingProgressBar;
    public Animator crossfade;

    public SoundObject buttonClick;

    public GameObject[] levelButtons;

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    public SoundObject titleMusic;
    private void Start()
    {
        InvokeRepeating("TitleMusic", 0.5f, titleMusic.soundClip.length);
        CheckForLevelsReached();
        //EventManager.OnLevelLoaded();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void CheckForLevelsReached()
    {
        if (PlayerPrefs.GetInt("LevelTwoReached") == 1)
        {
            levelButtons[0].SetActive(true);
        }
        if (PlayerPrefs.GetInt("LevelThreeReached") == 1)
        {
            levelButtons[1].SetActive(true);
        }
    }

    public void StartGameButton(string startingLevel)
    {
        GameManager.instance.SetLevelIndex(FindLevelIndex(startingLevel));        
        StartCoroutine(StartingGameSequence(startingLevel));
    }

    int FindLevelIndex(string level)
    {
        print(level);
        switch (level)
        {
            case "LevelOne":
                return 1;
            case "LevelTwo":
                return 2;
            case "LevelThree":
                return 3;
            default:
                return 0;
        }

    }

    void HideMenu()
    {
        menu.SetActive(false);
    }

    IEnumerator StartingGameSequence(string levelName)
    {
        //stop music
        AudioManager.Instance.StopMusic();

        //crossfade
        crossfade.SetTrigger("Close");
        yield return new WaitForSeconds(1f); //wait for crossfade to be open.
        
        if (levelName == "LevelOne")
        {
            SceneManager.LoadSceneAsync("OpeningStory");
        }
        else
        {
            SceneLoader.Instance.LoadGame(levelName);
        }
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

    public void ButtonClickSound()
    {
        AudioManager.Instance.PlaySFX(buttonClick);
    }
}
