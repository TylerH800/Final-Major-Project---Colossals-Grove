using UnityEngine;

public class Crossfade : MonoBehaviour
{
    private Animator animator;


    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        EventManager.LevelLoaded += Open;
    }

    private void OnDisable()
    {
        EventManager.LevelLoaded -= Open;
    }

    private void Open()
    {
        animator.SetTrigger("Open");
    }
}
