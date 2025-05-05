using Unity.AI.Navigation;
using UnityEngine;

public class SleepingColossal : MonoBehaviour
{
    private Animator anim;
    public GameObject col;

    private void OnEnable()
    {
        EventManager.ResetLevel += EMResetLevel;
    }

    private void OnDisable()
    {
        EventManager.ResetLevel -= EMResetLevel;
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    public void Move()
    {
        anim.SetTrigger("Move");
        col.SetActive(false);        
    }

    void EMResetLevel()
    {
        anim.SetTrigger("Reset");
        col.SetActive(true);
    }

   
}
