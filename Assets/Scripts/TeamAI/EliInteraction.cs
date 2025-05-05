using UnityEngine;
using UnityEngine.AI;

public class EliInteraction : MonoBehaviour
{
    private TeamMovement tm;
    private Animator anim;
    private NavMeshAgent agent;   
    private bool canPush = true;
 
    public LayerMask whatIsObstacle;

    private bool canIgnite = true;

    public float stopDistance = 1.5f;

    private void OnEnable()
    {
        EventManager.EliGate += EMSendToGate;
        EventManager.EliIgnite += EMSendToBarrel;
    }
    private void OnDisable()
    {
        EventManager.EliGate -= EMSendToGate;
        EventManager.EliIgnite -= EMSendToBarrel;
    }

    private void Start()
    {
        tm = GetComponent<TeamMovement>();  
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void EMSendToGate(Vector3 position)
    {
        Vector3 direction = (transform.position - position).normalized;
        Vector3 stopPos = position + direction * stopDistance;
        tm.state = TeamMovement.AIState.tasked;
        tm.agent.SetDestination(stopPos);        
    }

    private void EMSendToBarrel(Vector3 position)
    {
        Vector3 direction = (transform.position - position).normalized;
        Vector3 stopPos = position + direction * stopDistance;
        tm.state = TeamMovement.AIState.tasked;
        tm.agent.SetDestination(stopPos);       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (tm.state == TeamMovement.AIState.tasked)
        {
            GameObject parent = other.transform.root.gameObject;
            if (parent.CompareTag("Gate"))
            {
                if (canPush)
                {                   
                    anim.SetTrigger("PushGate");
                    canPush = false;

                    parent.GetComponent<FenceGate>().animator.SetBool("Open", true);
                    parent.GetComponent<FenceGate>().animator.SetBool("Close", false);
                    parent.GetComponent<NavMeshObstacle>().enabled = false;
                }
            }

            if (parent.CompareTag("Barrel") && canIgnite)
            {
                parent.layer = LayerMask.NameToLayer("InactiveObstacle");
                anim.SetTrigger("Ignite");
                transform.LookAt(parent.transform.position, Vector3.up);
                canIgnite = false;
                Invoke("IgniteCooldown", 4);
                StartCoroutine(parent.GetComponent<Barrel>().Explode());
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {        
        GameObject parent = other.transform.root.gameObject;

        if (parent.CompareTag("Gate"))
        {
            parent.GetComponent<FenceGate>().animator.SetBool("Close", true);
            parent.GetComponent<FenceGate>().animator.SetBool("Open", false);
            parent.GetComponent<NavMeshObstacle>().enabled = true;
            parent.gameObject.layer = LayerMask.NameToLayer("Obstacle");

            Invoke("CanPushCD", 2);
        }
    }

    void IgniteCooldown()
    {
        canIgnite = true;
    }

    void CanPushCD()
    {
        canPush = true;
    }

}
