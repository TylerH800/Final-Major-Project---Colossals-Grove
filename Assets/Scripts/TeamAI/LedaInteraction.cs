using UnityEngine;

public class LedaInteraction : MonoBehaviour
{
    private TeamMovement tm;
    public float stopDistance = 3f;

    #region events

    private void OnEnable()
    {
        EventManager.LedaBait += EMSendToNC;
    }

    private void OnDisable()
    {
        EventManager.LedaBait -= EMSendToNC;
    }

    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tm = GetComponent<TeamMovement>();  
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NeutralColossal") && tm.state == TeamMovement.AIState.tasked)
        {
            tm.state = TeamMovement.AIState.following;
            other.transform.root.GetComponent<NeutralColossal>().TriggerHit();
        }
    }

    private void EMSendToNC(Vector3 position)
    {
        print(position);
        Vector3 direction = (transform.position - position).normalized;
        Vector3 stopPos = position + direction * stopDistance;
        tm.state = TeamMovement.AIState.tasked;
        tm.agent.SetDestination(stopPos);       
    }
}
