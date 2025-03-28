using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public SoundObject[] walkSounds;
    public SoundObject[] crouchWalkSounds;
    public SoundObject[] runSounds;
    public SoundObject[] jumpSounds;
    
    public void Walk()
    {
        int i = Random.Range(0,walkSounds.Length);
        AudioManager.Instance.PlaySFX(walkSounds[i]);
    }

    public void CrouchWalk()
    {
        int i = Random.Range(0, crouchWalkSounds.Length);
        AudioManager.Instance.PlaySFX(crouchWalkSounds[i]);
    }

    public void Run()
    {
        int i = Random.Range(0, runSounds.Length);
        AudioManager.Instance.PlaySFX(runSounds[i]);
    }

    public void Jump()
    {
        int i = Random.Range(0, jumpSounds.Length);
        AudioManager.Instance.PlaySFX(jumpSounds[i]);
    }
}
