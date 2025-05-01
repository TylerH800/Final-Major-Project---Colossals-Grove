using UnityEngine;

public class TeamStartingProcess : MonoBehaviour
{
    public enum Character
    {
        Eli,
        Leda
    }

    public Character character;

    private void OnEnable()
    {        
        EventManager.PlayerStart += SetTransform;
    }

    private void OnDisable()
    {
        EventManager.PlayerStart -= SetTransform;
    }

    void SetTransform(Transform playerPos, Transform eliPos, Transform ledaPos)
    {
        if (character == Character.Eli)
        {
            transform.position = eliPos.position;
        }
        else if (character == Character.Leda)
        {
            transform.position = ledaPos.position;
        }
    }

}
