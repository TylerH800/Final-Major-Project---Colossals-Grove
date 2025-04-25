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

    public void StartGameButton()
    {
        HideMenu();
        ShowLoadingScreen();
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Gameplay"));
        scenesToLoad.Add(SceneManager.LoadSceneAsync("One", LoadSceneMode.Additive));
        StartCoroutine(LoadingScreen());
    }

    void HideMenu()
    {
        menu.SetActive(false);
    }

    void ShowLoadingScreen()
    {
        loadingInterface.SetActive(true);
    }

    IEnumerator LoadingScreen()
    {
        float totalProgress = 0;

        for (int i = 0;  i < scenesToLoad.Count; i++)
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
