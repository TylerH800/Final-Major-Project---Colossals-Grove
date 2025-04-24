using UnityEngine;

public class ObjectivePopupTrigger : MonoBehaviour
{
    public string newText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventManager.OnNewObjective(newText);
        }
    }
}
