using UnityEngine;
using UnityEngine.AI;

public class FenceGate : MonoBehaviour
{    
    public Animator animator;
    private NavMeshObstacle obstacle;
    public SoundObject openSound;
    public SoundObject closeSound;
    public LayerMask whatIsEli;

    public AudioSource source;

    private void Start()
    {
        animator = GetComponent<Animator>();
        obstacle = GetComponent<NavMeshObstacle>();
    }

    public void OpenSFX() => source.PlayOneShot(openSound.soundClip, openSound.volume);
    public void CloseSFX() => source.PlayOneShot(closeSound.soundClip, closeSound.volume);

    private void Update()
    {
        Collider[] e = Physics.OverlapSphere(transform.position, 3, whatIsEli);
        {
            foreach (Collider c in e)
            {           ;
                TeamMovement tm = c.GetComponent<TeamMovement>();
                if (tm.state == TeamMovement.AIState.following && c.gameObject.name == "Eli")
                {                   
                    animator.SetBool("Close", true);
                    animator.SetBool("Open", false);
                    Invoke("SetObstacle", 1f);
                    gameObject.layer = LayerMask.NameToLayer("Obstacle");
                }
            }
        }
    }

    void SetObstacle()
    {
        obstacle.enabled = true;
    }


}
