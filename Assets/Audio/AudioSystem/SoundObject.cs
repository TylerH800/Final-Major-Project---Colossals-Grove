using UnityEngine;

[CreateAssetMenu(fileName = "SoundTemplate", menuName = "ScriptableObjects/Audio/SoundTemplate")]
public class SoundObject : ScriptableObject
{
    public AudioClip soundClip;
    public float volume; //clip volume adjustable in editor, used to avoid editing raw clip volume in sound editor
    
}
