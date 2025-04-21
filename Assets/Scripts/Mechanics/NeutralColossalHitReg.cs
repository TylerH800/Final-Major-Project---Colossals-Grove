using UnityEngine;

public class NeutralColossalHitReg : MonoBehaviour
{
    private NeutralColossal nc;
    public AudioSource source;
    public SoundObject slam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nc = transform.root.GetComponent<NeutralColossal>();
    }

    public void HitReg() => nc.NormalHit();

    public void ReLayer() => transform.root.gameObject.layer = LayerMask.NameToLayer("Obstacle");

    public void SlamSFX() => source.PlayOneShot(slam.soundClip, slam.volume);
}
