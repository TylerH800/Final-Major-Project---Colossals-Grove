using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects")]
public class Dialogue : ScriptableObject
{
    public AudioClip speech;
    public enum characterIndex {mateo, eli, leda};
    public string subtitle;
}
