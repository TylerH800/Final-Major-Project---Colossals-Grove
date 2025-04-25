using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    public AudioMixer mixer;  

    public Slider masterVolumeSlider;
    public Slider sfxVolumeSlider; 
    public Slider musicVolumeSlider;

    public TextMeshProUGUI masterVolumeText;
    public TextMeshProUGUI musicVolumeText;
    public TextMeshProUGUI sfxVolumeText;

    private void Start()
    {
        SetAudioValues();
        SetUIValues();
    }

    #region Saving and Loading   

    void SetAudioValues()
    {
        //thanks to save data checks in awake when the game runs, i can simply set the mixer to the previously saved value
        mixer.SetFloat("MixerMasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("MasterVolume")) * 20);
        mixer.SetFloat("MixerMusicVolume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume")) * 20);
        mixer.SetFloat("MixerSFXVolume", Mathf.Log10(PlayerPrefs.GetFloat("SFXVolume")) * 20);       
    }

    void SetUIValues()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");        

        masterVolumeText.text = PlayerPrefs.GetFloat("MasterVolume").ToString("F2");
        musicVolumeText.text = PlayerPrefs.GetFloat("MusicVolume").ToString("F2");
        sfxVolumeText.text = PlayerPrefs.GetFloat("SFXVolume").ToString("F2");      
    }

    #endregion

    #region Settings

    public void SetMasterVolume(float value)
    {
        //sets mixer to inputted value as well as display text
        mixer.SetFloat("MixerMasterVolume", Mathf.Log10(value) * 20);
        masterVolumeText.text = value.ToString("F2");
        //saves value
        PlayerPrefs.SetFloat("MasterVolume", value);
    }
    public void SetSFXVolume(float value)
    {
        mixer.SetFloat("MixerSFXVolume", Mathf.Log10(value) * 20);
        sfxVolumeText.text = value.ToString("F2");
        PlayerPrefs.SetFloat("SFXVolume", value);
    }
    public void SetMusicVolume(float value)
    {
        mixer.SetFloat("MixerMusicVolume", Mathf.Log10(value) * 20);
        musicVolumeText.text = value.ToString("F2");
        PlayerPrefs.SetFloat("MusicVolume", value);
    }
    #endregion
}
