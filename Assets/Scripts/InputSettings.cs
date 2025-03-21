using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputSettings : MonoBehaviour
{
    public Slider xSensSlider;
    public Slider ySensSlider;
    public Toggle holdToCrouchToggle;

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
        print(PlayerPrefs.GetFloat("XSensitivity"));
        holdToCrouchToggle.isOn = PlayerPrefs.GetInt("HoldToCrouch") == 1;
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
    }


}
