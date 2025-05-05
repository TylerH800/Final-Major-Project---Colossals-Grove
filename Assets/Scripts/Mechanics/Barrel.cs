using System.Collections;
using Unity.VisualScripting;
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
    public GameObject explosionSrc;

    public float explosionRadius = 5;
    public LayerMask whatIsSleepingColossal;

    public AudioSource source;

    private void Start()
    {
        startPos = transform.position;
    }

    private void OnEnable()
    {
        EventManager.ResetLevel += EMResetLevel;
    }

    private void OnDisable()
    {
        EventManager.ResetLevel -= EMResetLevel;
    }

    public IEnumerator Explode()
    {        
        yield return new WaitForSeconds(smokeTime);

        //smoke
        Instantiate(smokeFX, transform.position, Quaternion.identity);        
        source.PlayOneShot(smoke.soundClip, smoke.volume);

        yield return new WaitForSeconds(igniteTime - smokeTime);

        //explosion
        Instantiate(explosionFX, transform.position, Quaternion.identity);
        Instantiate(explosionSrc, transform.position, Quaternion.identity);

        if (!Physics.CheckSphere(transform.position, explosionRadius, whatIsSleepingColossal))
        {
            GameObject obj = Instantiate(barrelPrefab, startPos + new Vector3(0, spawnOffset, 0), Quaternion.Euler(-90, 0, 0));
            obj.layer = LayerMask.NameToLayer("Obstacle");
        }

        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius, whatIsSleepingColossal);

        foreach (Collider col in hits)
        {            
            if (col.GetComponent<SleepingColossal>() != null)
            {              
                col.GetComponent<SleepingColossal>().Move();
            }
        }
        gameObject.SetActive(false);
    }

    void EMResetLevel()
    {
        transform.position = startPos;
    }


}
