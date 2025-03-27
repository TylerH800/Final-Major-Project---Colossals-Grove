using UnityEngine;

[CreateAssetMenu(fileName = "CharacterValues", menuName = "ScriptableObjects/Characters")]
public class CharacterValues : ScriptableObject
{
    public float walkSpeed, runSpeed, followDistance, runDistance, characterIndex; //chasing    
}
