using TMPro;
using UnityEngine;

public class ObjectiveDisplay : MonoBehaviour
{
    public TextMeshProUGUI objectiveText;
    public GameObject objectiveObject;
    public float displaySeconds;
    private bool onDisplay;
    public SoundObject pingSound;
    private void OnEnable()
    {
        EventManager.NewObjective += NewObjective;
    }
    private void OnDisable()
    {
        EventManager.NewObjective -= NewObjective;
    }

    private void Update()
    {
        if (onDisplay)
        {
            displaySeconds -= Time.deltaTime;
        }

        if (displaySeconds <= 0)
        {
            objectiveObject.SetActive(false);
            onDisplay = false;
        }
    }

    void NewObjective(string objective)
    {
        objectiveText.text = "New Objective: " + objective;
        objectiveObject.SetActive(true);
        onDisplay = true;   
    }
}
