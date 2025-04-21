using UnityEngine;

public class FenceGate : MonoBehaviour
{    
    public Animator animator;
    public SoundObject openSound;
    public SoundObject closeSound;

    public AudioSource source;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenSFX() => source.PlayOneShot(openSound.soundClip, openSound.volume);
    public void CloseSFX() => source.PlayOneShot(closeSound.soundClip, closeSound.volume);

    /*private void OnTriggerEnter(Collider other)
    {      
        if (other.CompareTag("Eli"))
        {
            animator.SetTrigger("Open");
        }
    }

    private void OnTriggerExit(Collider other)
    {        
        if (other.CompareTag("Eli"))
        {
            other.GetComponent<EliInteraction>().taskTrigger.enabled = false;
            animator.SetTrigger("Close");
            gameObject.layer = LayerMask.NameToLayer("Obstacle");
        }
    } */


}
