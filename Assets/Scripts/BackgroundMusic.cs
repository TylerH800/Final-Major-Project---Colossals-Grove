using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public SoundObject bgMusic;
    private void Start()
    {
        InvokeRepeating("BGMusic", 0, bgMusic.soundClip.length);
    }

    void BGMusic()
    {
        AudioManager.Instance.PlayMusic(bgMusic);
    }


}
