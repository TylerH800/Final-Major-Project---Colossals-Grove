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

    public SoundObject buttonClick;

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    public SoundObject titleMusic;
    private void Start()
    {
        InvokeRepeating("TitleMusic", 0.5f, titleMusic.soundClip.length);
    }

    public void StartGameButton(string startingLevel)
    {
        GameManager.instance.SetLevelIndex(FindLevelIndex(startingLevel));
        StartCoroutine(StartingGameSequence(startingLevel));
    }

    int FindLevelIndex(string level)
    {
        switch (level)
        {
            case "One":
                return 1;
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

        //scene loading
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Gameplay"));
        scenesToLoad.Add(SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive));

        ScenesList.scenesOpen.Add("Gameplay");
        ScenesList.scenesOpen.Add(levelName);

        print(ScenesList.scenesOpen);
        //loading progress bar
        loadingInterface.SetActive(true);

        float totalProgress = 0;

        for (int i = 0; i < scenesToLoad.Count; i++)
        {
            while (!scenesToLoad[i].isDone)
            {
                totalProgress += scenesToLoad[i].progress;
                loadingProgressBar.fillAmount = totalProgress / scenesToLoad.Count;
                yield return null;
            }
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
