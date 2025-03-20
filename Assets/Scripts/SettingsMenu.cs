using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class SettingsMenu : MonoBehaviour
{
    [Header("Sound")]
    public Slider masterVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider dialogueVolumeSlider;

    public Toggle subtitlesOn;

    [Header("Input")]
    public Slider xSensitivitySlider;
    public Slider ySensitivitySlider;

    [Header("Graphics")]
    public DropdownMenu resolution;
    public DropdownMenu graphicsQuality;
    public DropdownMenu screenMode;

    void Start()
    {
        SetUIValues();
    }

    void SetUIValues()
    {

    }
  


    #region Sound Settings
    public void SetMasterVolume()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value);
    }
    public void SetSFXVolume()
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
    }
    public void SetMusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
    }

    public void SetDialogueVolume()
    {
        PlayerPrefs.SetFloat("DialogueVolume", dialogueVolumeSlider.value);
    }

    #endregion
}
