using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private Animator anim;
    public SoundObject sound;

    private void OnEnable()
    {
        EventManager.GameOver += EMGameOver;
        anim = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        EventManager.GameOver -= EMGameOver;
    }


    void EMGameOver()
    {
        anim.SetTrigger("Reset");
    }

    public void ResetLevel()
    {
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        AudioManager.Instance.PlaySFX(sound);
        List<string> scenesToReload = new List<string>();

        foreach (string s in ScenesList.scenesOpen)
        {
            if (s != "Gameplay" && SceneManager.GetSceneByName(s).isLoaded)
            {
                AsyncOperation unload = SceneManager.UnloadSceneAsync(s);
                while (!unload.isDone)
                {
                    yield return null;
                }

                scenesToReload.Add(s); 
            }
        }
       
        foreach (string s in scenesToReload)
        {
            AsyncOperation load = SceneManager.LoadSceneAsync(s, LoadSceneMode.Additive);
  
            while (!load.isDone)
            {
                yield return null;
            }
        }

        GameManager.instance.SetPlayerPosition();
    }
}
