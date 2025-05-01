using UnityEngine;

public class StartingProcess : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.instance.SetLevelIndex(1);
        ResetPosition();
    }

    public void ResetPosition()
    {
        GameManager.instance.SetPlayerPosition();
    }

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
        transform.position = playerPos.position;       
    }
}
