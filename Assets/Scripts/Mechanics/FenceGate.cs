using UnityEngine;
using UnityEngine.AI;

public class FenceGate : MonoBehaviour
{    
    public Animator animator;
    public SoundObject openSound;
    public SoundObject closeSound;
    public LayerMask whatIsEli;

    public AudioSource source;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenSFX() => source.PlayOneShot(openSound.soundClip, openSound.volume);
    public void CloseSFX() => source.PlayOneShot(closeSound.soundClip, closeSound.volume);

    private void Update()
    {
        Collider[] e = Physics.OverlapSphere(transform.position, 3, whatIsEli);
        {
            foreach (Collider c in e)
            {
                print("team");
                TeamMovement tm = c.GetComponent<TeamMovement>();
                if (tm.state == TeamMovement.AIState.following && c.gameObject.name == "Eli")
                {
                    print("following");
                    animator.SetBool("Close", true);
                    animator.SetBool("Open", false);
                    GetComponent<NavMeshObstacle>().enabled = true;
                    gameObject.layer = LayerMask.NameToLayer("Obstacle");
                }
            }
        }
    }


}
