using UnityEngine;
using TMPro;

public class DebugText : MonoBehaviour
{
    public TextMeshProUGUI screenMode;
    public TextMeshProUGUI resolution;
    public TextMeshProUGUI graphicsQuality;
    public TextMeshProUGUI vsync;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        screenMode.text = Screen.fullScreenMode.ToString();
        resolution.text = Screen.currentResolution.ToString();

        int gq = QualitySettings.GetQualityLevel();
        switch (gq)
        {
            case 0:
                graphicsQuality.text = "Graphics Quality: Low";
                break;
            case 1:
                graphicsQuality.text = "Graphics Quality: Medium";
                break;
            case 2:
                graphicsQuality.text = "Graphics Quality: High";
                break;
        }

        switch (QualitySettings.vSyncCount)
        {
            case 0:
                vsync.text = "Vsync: Off";
                break;
            case 1:
                vsync.text = "Vsync: On";
                break;
        }

    }

}
