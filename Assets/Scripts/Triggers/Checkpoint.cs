using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int level;
    public string playerPrefsKey;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.SetLevelIndex(level);
            PlayerPrefs.SetInt(playerPrefsKey, 1);
            Destroy(gameObject);
        }
    }
}
