using UnityEngine;
using UnityEngine.AI;

public class WalkingColossal : MonoBehaviour
{
    public Transform[] walkPoints;
    private NavMeshAgent agent;
    private Animator anim;
    private int currentIndex = 0;
    private bool walkPointSet;  

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        ReachedWalkPoint();
        agent.SetDestination(walkPoints[0].position);
    }

    private void OnEnable()
    {
        EventManager.ResetLevel += EMResetLevel;
    }

    private void OnDisable()
    {
        EventManager.ResetLevel -= EMResetLevel;
    }

    private void Update()
    {
        if (!walkPointSet && agent.remainingDistance <= agent.stoppingDistance)
        {
            ReachedWalkPoint();        
        }

        if (walkPointSet && agent.remainingDistance >= agent.stoppingDistance)
        {
            walkPointSet = false;
        }
        
        anim.SetFloat("Velocity", agent.velocity.magnitude);
        //print(agent.destination);
    }

    private void ReachedWalkPoint()
    {
        if (walkPoints.Length == 0)
        {
            return;
        }

        currentIndex++;
        if (currentIndex >= walkPoints.Length)
        {
            currentIndex = 0;
        }     

        NavMeshHit pos; NavMesh.SamplePosition(walkPoints[currentIndex].position, out pos, 5, NavMesh.AllAreas);
        agent.SetDestination(pos.position);
        walkPointSet = true;
    }

    private void EMResetLevel()
    {
        ReachedWalkPoint();
        agent.SetDestination(walkPoints[0].position);
        transform.position = walkPoints[0].position;
    }
}
