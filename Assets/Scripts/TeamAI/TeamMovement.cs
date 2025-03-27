using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TeamMovement : MonoBehaviour
{
    private enum AIState
    {
        idle, following, tasked
    }

    private AIState state;

    public CharacterValues characterValues;
    private NavMeshAgent agent;
    private Animator anim;
    private Transform player;

    private bool hasTarget;
    private bool isWaiting = false;

    private Vector3 currentIdlePosition;

    #region Events

    private void OnEnable()
    {
        EventManager.ChangeAIToFollow.AddListener(EMChangeAIToFollow);
        EventManager.ChangeAIToFollow.AddListener(EMChangeAIToIdle);
    }
    private void OnDisable()
    {
        EventManager.ChangeAIToFollow.RemoveListener(EMChangeAIToFollow);
        EventManager.ChangeAIToFollow.RemoveListener(EMChangeAIToIdle);
    }

    void EMChangeAIToFollow(int character)
    {
        if (character == characterValues.characterIndex)
        {
            state = AIState.following;
        }
    }

    void EMChangeAIToIdle(int character)
    {
        if (character == characterValues.characterIndex)
        {
            state = AIState.idle;
        }
    }

    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        SetToIdle();
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine();
        SetSpeed();
        Animation();
    }

    void Animation()
    {
        anim.SetFloat("Velocity", agent.velocity.magnitude);
    }
    void SetToIdle()
    {
        agent.SetDestination(transform.position);
        state = AIState.following;
    }

    void SetToFollow()
    {
        hasTarget = false;
        state = AIState.following;
    }

    void SetToTask()
    {
        state = AIState.tasked;
    }

    void StateMachine()
    {
        if (state == AIState.following)
        {
            FollowPlayer();
        }
        if (state == AIState.tasked)
        {
            CheckForTaskArrival();
        }

    }

    void SetSpeed()
    {
        //setting speed
        if (Vector3.Distance(agent.destination, gameObject.transform.position) >= characterValues.runDistance)
        {
            agent.speed = characterValues.runSpeed;
        }
        else if (Vector3.Distance(agent.destination, gameObject.transform.position) >= characterValues.followDistance)
        {
            agent.speed = characterValues.walkSpeed;
        }
    }

    void FollowPlayer()
    {        
        if (Vector3.Distance(player.transform.position, transform.position) >= characterValues.followDistance)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            agent.ResetPath();
        }

    }
    bool RandomNavmeshLocation(float radius, Vector3 centre, out Vector3 result)
    {
        Vector3 randomPoint = centre + Random.insideUnitSphere * radius; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            //if the random point is on or close enough to the mesh, the position is returned and destination is set
            //if not, the destinatin isn't set and it will try again next frame
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }

    void DoTask(Vector3 taskPosition)
    {       
        NavMeshHit hit;
        NavMesh.SamplePosition(taskPosition, out hit, 1f, NavMesh.AllAreas);
        agent.SetDestination(hit.position);
    }

    void CheckForTaskArrival()
    {

    }
}
