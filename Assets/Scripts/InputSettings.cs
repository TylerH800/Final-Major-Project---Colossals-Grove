using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class InputSettings : MonoBehaviour
{
    public Slider xSensSlider;
    public Slider ySensSlider;
    public Toggle holdToCrouchToggle;
    public Toggle holdToSprintToggle;

    public TextMeshProUGUI xSensText;
    public TextMeshProUGUI ySensText;    

    void Start()
    {
        SetUIValues();
    }

    void SetUIValues()
    {
        xSensSlider.value = PlayerPrefs.GetFloat("XSensitivity");
        ySensSlider.value = PlayerPrefs.GetFloat("YSensitivity");        
        holdToCrouchToggle.isOn = PlayerPrefs.GetInt("HoldToCrouch") == 1;
        holdToSprintToggle.isOn = PlayerPrefs.GetInt("HoldToSprint") == 1;
    }

    public void SetXSensitivity(float value)
    {
        PlayerPrefs.SetFloat("XSensitivity", value);
        xSensText.text = value.ToString("F2");
    }

    public void SetYSensitivity(float value)
    {
        PlayerPrefs.SetFloat("YSensitivity", value);
        ySensText.text = value.ToString("F2");

    }

    public void HoldToCrouch(bool value)
    {
        PlayerPrefs.SetInt("HoldToCrouch", value ? 1 : 0);
        EventManager.OnInputSettingsChanged();
    }

    public void HoldToSprint(bool value)
    {
        PlayerPrefs.SetInt("HoldToSprint", value ? 1 : 0);
        EventManager.OnInputSettingsChanged();
    }


}
