using Unity.VisualScripting;
using UnityEngine;

public class NeutralColossal : MonoBehaviour
{
    private Animator anim;
    public Transform targetPos;
    private bool inLedaAttack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void TriggerHit()
    {
        anim.SetTrigger("LedaHit");
        inLedaAttack = true;
    }

    public void NormalHit()
    {
        EventManager.OnGameOver();
        print("game over");
        anim.SetBool("LedaHit", false);
        anim.SetBool("NormalHit", false);
        inLedaAttack = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (inLedaAttack)
        {
            return;
        }

        if (other.gameObject.CompareTag("Eli") || other.gameObject.CompareTag("Leda"))
        {            
            if (other.gameObject.GetComponent<TeamMovement>().state != TeamMovement.AIState.tasked)
            {
                anim.SetBool("NormalHit", true);
                print(inLedaAttack);
            }            
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            anim.SetBool("NormalHit", true);
            print(inLedaAttack);
        }
        
    }
}
