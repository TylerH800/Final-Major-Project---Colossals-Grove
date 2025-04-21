using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Android;

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
        print(agent.destination);
    }

    private void ReachedWalkPoint()
    {
        
        currentIndex++;
        if (currentIndex >= walkPoints.Length)
        {
            currentIndex = 0;
        } 
        print("Moving to walkpoint" + currentIndex);

        NavMeshHit pos; NavMesh.SamplePosition(walkPoints[currentIndex].position, out pos, 5, NavMesh.AllAreas);
        agent.SetDestination(pos.position);
        walkPointSet = true;
    }  
}
