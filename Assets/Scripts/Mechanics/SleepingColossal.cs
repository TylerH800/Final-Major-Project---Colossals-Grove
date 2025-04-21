using Unity.AI.Navigation;
using UnityEngine;

public class SleepingColossal : MonoBehaviour
{
    private Animator anim;
    public GameObject col;    

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    public void Move()
    {
        anim.SetTrigger("Move");
        col.SetActive(false);
        //Invoke("BuildMesh", 3);
    }

   
}
