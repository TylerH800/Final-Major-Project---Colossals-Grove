using NUnit.Framework;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource sfxSource;
    public AudioSource musicSource;
    public AudioSource dialogueSource;

    void Awake()
    {
        //singleton design pattern
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

    void Start()
    {
        GetSaveData();
    }

    void GetSaveData()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            PlayerPrefs.GetFloat("MasterVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("MasterVolume", 1);
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            PlayerPrefs.GetFloat("SFXVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("SFXVolume", 1);
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.GetFloat("MusicVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("MusicVolume", 1);
        }

        if (PlayerPrefs.HasKey("DialogueVolume"))
        {
            PlayerPrefs.GetFloat("DialogueVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("DialogueVolume", 1);
        }
    }

    //plays the sound provided with its clip volume
    public void PlaySFX(SoundObject soundObject)
    {
        sfxSource.PlayOneShot(soundObject.soundClip, soundObject.volume);
    }

    //does the same but takes in a looping bool
    public void PlayMusic(SoundObject soundObject, bool looping)
    {
        if (looping)
        {
            StartCoroutine(LoopMusic(soundObject));
        }
        else
        {
            musicSource.PlayOneShot(soundObject.soundClip, soundObject.volume);
        }
    }

    //takes in the inputted clip and loops it based on it's length
    IEnumerator LoopMusic(SoundObject soundObject)
    {
        while (true)
        {
            musicSource.PlayOneShot(soundObject.soundClip, soundObject.volume);
            yield return new WaitForSeconds(soundObject.soundClip.length);
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
