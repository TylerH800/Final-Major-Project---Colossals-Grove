using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private float fps;
    public TextMeshProUGUI fpsCounterText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("GetFPS", 0, 1f);
    }

    void GetFPS()
    {
        fps = (int) (1f / Time.unscaledDeltaTime);
        fpsCounterText.text = fps.ToString();
    }

}
