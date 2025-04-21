using UnityEngine;

public class PushObjects : MonoBehaviour
{
    public float pushPower = 4f;
    private Animator anim;


    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {        
        Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
        
        if (rb == null || rb.isKinematic)
        {
            
            return;
        }

        if (hit.moveDirection.y < -0.3f)
        {
            return;
        }      

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        Vector3 collisionPoint = hit.point;

        rb.AddForceAtPosition(pushDir * pushPower, collisionPoint, ForceMode.Impulse);
    }
}
