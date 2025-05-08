using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    public List<string> openScenes = new List<string>();
    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadGame(string level) => StartCoroutine(Load(level));

    public IEnumerator Load(string levelName)
    {

        openScenes.Add("Gameplay");
        openScenes.Add(levelName);

        //scene loading
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Gameplay"));
        scenesToLoad.Add(SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive));

        for (int i = 0; i < scenesToLoad.Count; i++)
        {
            while (!scenesToLoad[i].isDone)
            {                
                yield return null;
            }

        }

        yield return new WaitForSeconds(0.5f);
        GameManager.instance.SetPlayerPosition();

        print("Loaded");
    }
}
