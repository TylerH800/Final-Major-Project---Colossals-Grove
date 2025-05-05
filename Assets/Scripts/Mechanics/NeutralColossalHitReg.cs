using UnityEngine;

public class NeutralColossalHitReg : MonoBehaviour
{
    private NeutralColossal nc;
    public AudioSource source;
    public SoundObject slam;
    public GameObject collider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nc = transform.root.GetComponent<NeutralColossal>();
    }

    public void HitReg() => nc.NormalHit();

    public void ReLayer() => transform.root.gameObject.layer = LayerMask.NameToLayer("Obstacle");

    public void SlamSFX() => source.PlayOneShot(slam.soundClip, slam.volume);

    public void RemoveCollider() => collider.SetActive(false);

    public void AddCollider() => collider.SetActive(true);
    

        
}
