using UnityEngine;

public class WalkingColossalStepping : MonoBehaviour
{
    public Transform stepPos1;
    public Transform stepPos2;
    public float hitRadius;
    public LayerMask hitLayerMask;

    public void Step()
    {
        if (Physics.CheckSphere(stepPos1.position, hitRadius, hitLayerMask))
        {
            EventManager.OnGameOver();
            print("game over");
        }

        if (Physics.CheckSphere(stepPos2.position, hitRadius, hitLayerMask))
        {
            EventManager.OnGameOver();
            print("game over");
        }
    }
}
