using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TeamMovement : MonoBehaviour
{
    public enum AIState
    {
        idle, following, tasked, lost
    }

    public AIState state;

    public CharacterValues characterValues;
    [HideInInspector] public NavMeshAgent agent;
    private Animator anim;
    private Transform player;     

    private Vector3 currentIdlePosition;

    public float wanderTime;    
    public float wanderRadius;
    private float timer;
    public Transform wanderPos;

    #region Events

    private void OnEnable()
    {
        if (characterValues.characterIndex == 0) //eli index
        {
            EventManager.ChangeEliAIMode += EMChangeAIMode;
        }
        else
        {
            EventManager.ChangeLedaAIMode += EMChangeAIMode;
        }
    }
    private void OnDisable()
    {
        if (characterValues.characterIndex == 0) //eli index
        {
            EventManager.ChangeEliAIMode -= EMChangeAIMode;
        }
        else
        {
            EventManager.ChangeLedaAIMode -= EMChangeAIMode;
        }
    }

    void EMChangeAIMode() //toggles between idle and follow
    {
        if (state == AIState.idle)
        {
            state = AIState.following;
        }
        else if (state == AIState.following)
        {
            state = AIState.idle;
            agent.SetDestination(transform.position);
        }
        else
        {
            state = AIState.following;
        }
    }

    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        SetToFollow();
        StartCoroutine(EnableAgent());

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
        state = AIState.following;
    }

    public void SetToTask()
    {
        state = AIState.tasked;
    }

    void StateMachine()
    {
        if (state == AIState.following)
        {
            FollowPlayer();
        }   
        
        if (state == AIState.lost)
        {
            Wander();
        }
    }

    void SetSpeed()
    {
        if (!agent.enabled)
        {
            return;
        }

        if (state == AIState.lost)
        {
            agent.speed = characterValues.walkSpeed;
        }

        //setting speed
        else if (Vector3.Distance(agent.destination, gameObject.transform.position) >= characterValues.runDistance)
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
        if (!agent.enabled)
        {
            return;
        }

        if (Vector3.Distance(player.transform.position, transform.position) >= characterValues.followDistance)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            agent.ResetPath();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WanderTrigger")
        {
            state = AIState.lost;
        }
    }

    void Wander()
    {
        if (Vector3.Distance(transform.position, player.position) <= wanderRadius)
        {
            SetToFollow();
        }

        timer += Time.deltaTime;

        if (timer > wanderTime)
        {
            Vector3 pos;
            if (RandomNavmeshLocation(wanderRadius, wanderPos.position, out pos))
            {
                agent.SetDestination(pos);
                timer = 0;
            }
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

    IEnumerator EnableAgent()
    {
        yield return new WaitForSeconds(0.5f);

        while (!NavMesh.SamplePosition(agent.transform.position, out var hit, 1f, NavMesh.AllAreas))
        {
            print("No mesh this frame");
            yield return null;
        }

        agent.enabled = true;
    }
}
