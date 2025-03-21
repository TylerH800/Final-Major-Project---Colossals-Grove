using UnityEngine;
using System.Collections;

public class playsound : MonoBehaviour
{
    public SoundObject music;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LoopMusic(music));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoopMusic(SoundObject soundObject)
    {
        while (true)
        {
            AudioManager.Instance.PlayMusic(music);
            yield return new WaitForSeconds(soundObject.soundClip.length);
        }
    }
}
