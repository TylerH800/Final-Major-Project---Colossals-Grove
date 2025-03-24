using System.Collections.Generic;
using UnityEngine;

public class SaveDataCheck : MonoBehaviour
{
    //this class is used to set all default playerprefs values in one place

    private void Awake()
    {
        CheckAudioSaveData();
        CheckGraphicalSaveData();
        CheckInputSaveData();
    }

    void CheckAudioSaveData()
    {
        SavedFloatCheck("MasterVolume", 0.5f);
        SavedFloatCheck("MusicVolume", 0.5f);
        SavedFloatCheck("SFXVolume", 0.5f);
        SavedFloatCheck("DialogueVolume", 0.5f);
        SavedIntCheck("Subtitles", 1);
    }

    void CheckGraphicalSaveData()
    {
        Resolution[] AllResolutions = Screen.resolutions;

        //creates a temporary string list for the dropdown
        List<string> resolutionList = new List<string>();
        string newRes;

        foreach (Resolution res in AllResolutions)
        {
            //creates a string for nice formatting
            newRes = res.width.ToString() + "x" + res.height.ToString();

            //due to duplicates, if the string isnt alrteady there, it will be added to the string list and resolution list (it will have the same index in both)
            if (!resolutionList.Contains(newRes))
            {
                resolutionList.Add(newRes);
                
            }
        }
        SavedIntCheck("Resolution", resolutionList.Count - 1);        
        SavedIntCheck("GraphicsQuality", 1);
        SavedIntCheck("ScreenMode", 0);
        SavedIntCheck("ShowFPS", 0);
        SavedIntCheck("Vsync", 0);
        SavedIntCheck("ShowDebug", 0);
    }

    void CheckInputSaveData()
    {
        SavedFloatCheck("XSensitivity", 0.5f);
        SavedFloatCheck("YSensitivity", 0.5f);
        SavedIntCheck("HoldToCrouch", 0);
    }

    public void SavedFloatCheck(string key, float defaultValue) //used to avoid repetitive code checking for playerprefs values
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetFloat(key, defaultValue);            
        }
    }

    public void SavedIntCheck(string key, int defaultValue) //used to avoid repetitive code checking for playerprefs values
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetInt(key, defaultValue);
        }
    }
}
