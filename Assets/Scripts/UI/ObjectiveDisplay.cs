using TMPro;
using UnityEngine;

public class ObjectiveDisplay : MonoBehaviour
{
    public TextMeshProUGUI objectiveText;
    public GameObject objectiveObject;
    public float displaySeconds;
    private float timeLeft;
    private bool onDisplay;
    public SoundObject notificationSound;
    private void OnEnable()
    {
        EventManager.NewObjective += NewObjective;
        timeLeft = displaySeconds;
    }
    private void OnDisable()
    {
        EventManager.NewObjective -= NewObjective;
    }

    private void Update()
    {
        if (onDisplay)
        {
            timeLeft -= Time.deltaTime;
        }

        if (timeLeft <= 0)
        {
            objectiveObject.SetActive(false);
            onDisplay = false;
        }
    }

    void NewObjective(string objective)
    {     
        AudioManager.Instance.PlaySFX(notificationSound);
        objectiveText.text = "New Objective: " + objective;
        objectiveObject.SetActive(true);
        onDisplay = true;
        timeLeft = displaySeconds;
    }
}
