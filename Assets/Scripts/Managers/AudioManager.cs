using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    //public AudioMixer mixer;
    public AudioSource sfxSource;
    public AudioSource musicSource;

    public float fadeRate = 1f;

    private bool fadingIn;
    private bool fadingOut;

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


    #region Playing Sound
    //plays the sound provided with its clip volume
    public void PlaySFX(SoundObject soundObject)
    {
        sfxSource.PlayOneShot(soundObject.soundClip, soundObject.volume);
    }

    //does the same but takes in a looping bool
    public void PlayMusic(SoundObject soundObject)
    {
        musicSource.clip = soundObject.soundClip;
        musicSource.Play();
        StartCoroutine(FadeInMusic());
    }

    public void StopMusic()
    {
        StartCoroutine(FadeOutMusic());
    }
    #endregion

    public IEnumerator FadeOutMusic()
    {
        fadingOut = true;
        fadingIn = false;

        //print("Start fading out");
        while (musicSource.volume > 0)
        {
            if (fadingIn)
            {
                break;
            }
            musicSource.volume = Mathf.MoveTowards(musicSource.volume, 0, fadeRate * Time.deltaTime);           
            yield return null;
        }

        musicSource.Stop();
        musicSource.volume = 0;
        fadingOut = false;
        //print("Finished fading out");

    }

    public IEnumerator FadeInMusic()
    {
        fadingIn = true;
        fadingOut = false;
        musicSource.volume = 0;
        //print("Start fading in");


        while (musicSource.volume < 1)
        {
            if (fadingOut)
            {
                break;
            }
            musicSource.volume = Mathf.MoveTowards(musicSource.volume, 1, fadeRate * Time.deltaTime);
            //print(musicSource.volume);
            yield return null;
        }
        //print("Finished fading in");
        fadingIn = false;
        musicSource.volume = 1;
    }
}
