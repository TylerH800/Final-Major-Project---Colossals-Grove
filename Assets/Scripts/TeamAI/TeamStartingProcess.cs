using UnityEngine;
using UnityEngine.AI;

public class TeamStartingProcess : MonoBehaviour
{
    public enum Character
    {
        Eli,
        Leda
    }

    public Character character;
    private NavMeshAgent agent;
    private TeamMovement tm;

    private void OnEnable()
    {        
        EventManager.PlayerStart += SetTransform;
        agent = GetComponent<NavMeshAgent>();
        tm = GetComponent<TeamMovement>();
    }

    private void OnDisable()
    {
        EventManager.PlayerStart -= SetTransform;
    }

    void SetTransform(Transform playerPos, Transform eliPos, Transform ledaPos)
    {       
        if (character == Character.Eli)
        {
            agent.Warp(eliPos.position);
            //print("New eli pos: " + agent.transform.position);

        }
        else if (character == Character.Leda)
        {
            agent.Warp(ledaPos.position);
            //print("New leda pos: " + agent.transform.position);
        }

        if (tm.state != TeamMovement.AIState.lost)
        {
            tm.state = TeamMovement.AIState.following;
        }

    }

}
