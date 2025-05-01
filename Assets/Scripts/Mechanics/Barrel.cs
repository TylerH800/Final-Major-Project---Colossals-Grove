using System.Collections;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    private Rigidbody rb;
    private float igniteTime = 6;
    private float smokeTime = 1.5f;
    private Vector3 startPos;
    public float spawnOffset = 15f;
    public GameObject explosionFX;
    public GameObject smokeFX;
    public GameObject barrelPrefab;

    public SoundObject smoke;
    public SoundObject explosion;

    public float explosionRadius = 5;
    public LayerMask whatIsSleepingColossal;

    public AudioSource source;

    private void Start()
    {
        startPos = transform.position;
    }

    public IEnumerator Explode()
    {
        print("Explode");
        yield return new WaitForSeconds(smokeTime);
        Instantiate(smokeFX, transform.position, Quaternion.identity);        
        source.PlayOneShot(smoke.soundClip, smoke.volume);
        yield return new WaitForSeconds(igniteTime - smokeTime);

        Instantiate(explosionFX, transform.position, Quaternion.identity);    
        source.PlayOneShot(explosion.soundClip, explosion.volume);

        if (!Physics.CheckSphere(transform.position, explosionRadius, whatIsSleepingColossal))
        {
            //print("no hit");
            GameObject obj = Instantiate(barrelPrefab, startPos + new Vector3(0, spawnOffset, 0), Quaternion.Euler(-90, 0, 0));
            obj.layer = LayerMask.NameToLayer("Obstacle");
        }
        

        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius, whatIsSleepingColossal);

        if (hits != null)
        {
            foreach (Collider col in hits)
            {
                print(col.name);
                if (col.GetComponent<SleepingColossal>() != null)
                {
                    col.GetComponent<SleepingColossal>().Move();
                }
            }
        }
        else
        {
            
        }

        Destroy(gameObject);
    }


}
