using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System.Collections.Generic;

public class GraphicsSettings : MonoBehaviour
{
    public UnityEvent settingsApplied; //used to update the debug text
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown screenModeDropdown;
    public TMP_Dropdown graphicsQualityDropdown;
    public Toggle fpsCounterToggle;
    public Toggle vsyncToggle;
    public Toggle showDebugToggle;

    private Resolution[] AllResolutions;
    private List<Resolution> displayedResolutions = new List<Resolution>();

    public GameObject fpsCounterObject;
    public GameObject debug;

    private int selectedResolution;
    private int screenMode;
    private int graphicsQuality;
    private bool showfps;
    private bool vsync;
    private bool showDebug;

    private void OnEnable()
    {
        EventManager.dataCheckDone += LoadSettings;
    }

    private void OnDisable()
    {
        EventManager.dataCheckDone -= LoadSettings;
    }

    private void Awake()
    {
        if (SaveDataCheck.checkDone)
        {
            LoadSettings();
        }
    }

    private void Start()
    {
        ApplySettings();
    }

    void LoadSettings()
    {

        // -------Resolution----------
        //creates an array of every available screen resolution
        AllResolutions = Screen.resolutions;

        //creates a temporary string list for the dropdown
        List<string> resolutionStringList = new List<string>();
        string newRes;

        foreach (Resolution res in AllResolutions)
        {
            //creates a string for nice formatting
            newRes = res.width.ToString() + "x" + res.height.ToString();
            //print(newRes);

            //due to duplicates, if the string isnt alrteady there, it will be added to the string list and resolution list (it will have the same index in both)
            if (!resolutionStringList.Contains(newRes))
            {
                resolutionStringList.Add(newRes);
                displayedResolutions.Add(res);
            }
        }

        if (resolutionDropdown != null) //for scenes with no dropdown
        {
            resolutionDropdown.ClearOptions();
            resolutionDropdown.AddOptions(resolutionStringList);
        }

        selectedResolution = PlayerPrefs.GetInt("Resolution");
        //print("Resolution loaded to " + selectedResolution);

        screenMode = PlayerPrefs.GetInt("ScreenMode");
        //print("Screenmode loaded to " + screenMode);

        graphicsQuality = PlayerPrefs.GetInt("GraphicsQuality");
        //print("Graphics Quality loaded to " + graphicsQuality);

        showfps = PlayerPrefs.GetInt("ShowFPS") == 1;
        //print("showfps = " + showfps);

        vsync = PlayerPrefs.GetInt("Vsync") == 1;
        //print("vsync = " + vsync);

        showDebug = PlayerPrefs.GetInt("ShowDebug") == 1;
        //print("showdebug = " + showDebug);
    }

    void ApplySettings()
    {
        //------------Resolution------------
        if (selectedResolution < displayedResolutions.Count)
        {                      
            //sets resolution to current index
            Resolution temp = displayedResolutions[selectedResolution];
            Screen.SetResolution(temp.width, temp.height, Screen.fullScreenMode);
            //print("Resolution applied to index " + selectedResolution);
            resolutionDropdown.value = selectedResolution;
            //print(temp.width + " " +  temp.height);
            //print(resolutionDropdown.value);
        }

        //-----------ScreenMode-------------

        switch (screenMode)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }
        screenModeDropdown.value = screenMode;
        //print(screenMode);

        //------------Graphics Quality-------------
        QualitySettings.SetQualityLevel(graphicsQuality, false);
        graphicsQualityDropdown.value = graphicsQuality;
        //print("Graphics Quality applied to " + graphicsQuality);

        //------------vsync------------        
        QualitySettings.vSyncCount = vsync ? 1 : 0;
        vsyncToggle.isOn = vsync;
        //print("Vsync on = " + vsync);

        //------------showfps------------
        fpsCounterObject.SetActive(showfps);
        fpsCounterToggle.isOn = showfps;
        //print("Showfps on = " + showfps);

        //------------showdebug-------------
        debug.SetActive(showDebug);
        showDebugToggle.isOn = showDebug;
        settingsApplied.Invoke();
    }

    public void SetResolution(int index)
    {
        selectedResolution = index; //the selected resolutions index in the dropdown matches that of the shortened list        
        ApplySettings();
        SaveSettings();
    }

    public void SetScreenMode(int index)
    {
        screenMode = index;
        ApplySettings();
        SaveSettings();
    }

    public void SetGraphicsQuality(int index)
    {
        graphicsQuality = index;
        ApplySettings();
        SaveSettings();
    }

    public void SetVsync(bool index)
    {
        vsync = index;
        ApplySettings();
        SaveSettings();
    }

    public void SetShowFps(bool index)
    {
        showfps = index;
        ApplySettings();
        SaveSettings();
    }

    public void SetShowDebug(bool index)
    {
        showDebug = index;
        ApplySettings();
        SaveSettings();
    }

    void SaveSettings()
    {
        //saves current index
        PlayerPrefs.SetInt("Resolution", selectedResolution);
        //print("Resolution index saved to " + selectedResolution);

        PlayerPrefs.SetInt("ScreenMode", screenMode);
        //print("Screenmode set to " + screenMode);

        PlayerPrefs.SetInt("GraphicsQuality", graphicsQuality);
        //print("Graphics quality saved to " + graphicsQuality);

        PlayerPrefs.SetInt("Vsync", vsync ? 1 : 0);
        //print("Vysnc saved to " + vsync);

        PlayerPrefs.SetInt("ShowFPS", showfps ? 1 : 0);
        //print("ShowFPS saved to " + showfps);

        PlayerPrefs.SetInt("ShowDebug", showDebug ? 1 : 0);
        //print("ShowDebug saved to " + showDebug);
    }
}

