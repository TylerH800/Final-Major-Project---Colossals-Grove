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
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Eli") || other.gameObject.CompareTag("Leda"))
        {
            if (inLedaAttack)
            {
                return;
            }
            anim.SetBool("NormalHit", true);
            print(inLedaAttack);
        }
    }
}
