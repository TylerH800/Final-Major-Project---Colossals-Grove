using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePartLoader : MonoBehaviour
{
    private bool isLoaded;
    private bool shouldLoad;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isLoaded = SceneLoadedCheck();        
    }

    // Update is called once per frame
    void Update()
    {
        TriggerCheck();   
    }

    void TriggerCheck()
    {
        if (shouldLoad && !SceneLoadedCheck())
        {
            LoadScene();
        }
        else
        {
            shouldLoad = false;
            UnloadScene();
        }
    }

    void LoadScene()
    {
        if (!isLoaded)
        {       
            SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            SceneLoader.Instance.openScenes.Add(gameObject.name);
            isLoaded = true;
        }        
    }

    void UnloadScene()
    {
        if (isLoaded)
        {
            SceneManager.UnloadSceneAsync(gameObject.name);
            SceneLoader.Instance.openScenes.Remove(gameObject.name);
            isLoaded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))            
        {    
            shouldLoad = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shouldLoad = false;
        }
    }

    bool SceneLoadedCheck()
    {
        foreach (string s in SceneLoader.Instance.openScenes)
        {
            if (s == gameObject.name)
            {
                //print(gameObject.name + " is open");
                return true;
            }
        }

        return false;
    }
}
