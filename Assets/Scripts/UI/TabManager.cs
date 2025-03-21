using UnityEngine;

public class TabManager : MonoBehaviour
{
    public GameObject[] tabs;
    public void ActivateTab(GameObject active)
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            tabs[i].SetActive(false);
        }

        active.SetActive(true);
    }

    public void OnEnable()
    {
        for (int i = 0;i < tabs.Length; i++)
        {
            tabs[i].SetActive(false);
        }
    }

    public void OnDisable()
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            tabs[i].SetActive(false);
        }
    }
}
