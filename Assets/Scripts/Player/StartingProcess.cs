using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class StartingProcess : MonoBehaviour
{
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
        StartCoroutine(CheckPos(playerPos.position));
        //print("New player pos : " + transform.position);
    }

    IEnumerator CheckPos(Vector3 pos)
    {
        while (transform.position != pos)
        {
            transform.position = pos;
            yield return null;
        }
    }
}
