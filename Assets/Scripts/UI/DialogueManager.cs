using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public enum Story { start, end }

    public Story story;
    public TextMeshProUGUI dialogueText;
    public string[] lines;
    public float textSpeed;
    private int index;
    private bool typing;

    public SoundObject typingSound;
    private AudioSource source;

    public GameObject inputGuide;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueText.text = string.Empty;
        StartDialogue();
        
        source = GetComponent<AudioSource>();
        source.volume = typingSound.volume;
        source.clip = typingSound.soundClip;

    }

    private void Update()
    {
        Sound();
    }

    public void ProgressLine(InputAction.CallbackContext ct)
    {
        if (ct.performed)
        {
            if (dialogueText.text == lines[index])
            {
                NextLine();
            }
            else
            {
                typing = false;
                StopAllCoroutines();
                dialogueText.text = lines[index];
            }
        }        
    }

    public void SkipToGame(InputAction.CallbackContext ct)
    {        
        if (ct.performed)
        {
            EndDialogue();
        }       
    }
    

    void Sound()
    {
        if (typing && !source.isPlaying)
        {
            source.Play();
        }
        if (!typing && source.isPlaying)
        {
            source.Stop();
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        typing = true;
        foreach (char c in lines[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        typing = false;
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {                
        source.Stop();
        typing = false;
        dialogueText.text = string.Empty;
        dialogueText.gameObject.SetActive(false);
        inputGuide.SetActive(false);
        if (story == Story.start)
        {
            SceneLoader.Instance.LoadGame("LevelOne");
        }
        else
        {
            SceneLoader.Instance.openScenes.Clear();
            SceneManager.LoadSceneAsync("Frontend");
        }
    }
}
