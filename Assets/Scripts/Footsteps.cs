using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource source;
    public SoundObject[] walkSounds;
    public SoundObject[] crouchWalkSounds;
    public SoundObject[] runSounds;
    public SoundObject[] jumpSounds;
    
    public void Walk()
    {
        int i = Random.Range(0,walkSounds.Length);
        //AudioManager.Instance.PlaySFX(walkSounds[i]);
        source.PlayOneShot(walkSounds[i].soundClip, walkSounds[i].volume);
    }

    public void CrouchWalk()
    {
        int i = Random.Range(0, crouchWalkSounds.Length);
        //AudioManager.Instance.PlaySFX(crouchWalkSounds[i]);
        source.PlayOneShot(crouchWalkSounds[i].soundClip, crouchWalkSounds[i].volume);
    }

    public void Run()
    {
        int i = Random.Range(0, runSounds.Length);
        //AudioManager.Instance.PlaySFX(runSounds[i]);
        source.PlayOneShot(runSounds[i].soundClip, runSounds[i].volume);
    }

    public void Jump()
    {
        int i = Random.Range(0, jumpSounds.Length);
        //AudioManager.Instance.PlaySFX(jumpSounds[i]);
        source.PlayOneShot(jumpSounds[i].soundClip, jumpSounds[i].volume);
    }
}
