using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum LevelState
    {

    }

    public Transform[] playerSpawnPositions;   
    public Transform[] eliSpawnPositions;
    public Transform[] ledaSpawnPositions;

    public int currentLevelIndex;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetLevelIndex(int index)
    {
        currentLevelIndex = index;
        print(index);
    }
    public void SetPlayerPosition()
    {    
        print(currentLevelIndex + " 2");
        EventManager.OnPlayerStart(playerSpawnPositions[currentLevelIndex], eliSpawnPositions[currentLevelIndex], ledaSpawnPositions[currentLevelIndex]);
    }
}
