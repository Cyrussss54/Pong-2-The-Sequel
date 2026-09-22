using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    [Header("Audio Configurations")]
    public AudioSource menuAudioSource;
    public AudioClip moveSelectionSound;   
    public AudioClip selectConfirmSound;  

    private GameObject lastSelectedObject;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 

        GameObject firstButton = GameObject.Find("Start Game");
        if (firstButton == null) firstButton = GameObject.Find("Start");

        if (firstButton != null)
        {
            EventSystem.current.SetSelectedGameObject(firstButton);
            lastSelectedObject = firstButton;
        }
    }

    void Update()
    {
        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;
        if (currentSelected != lastSelectedObject && currentSelected != null)
        {
            if (currentSelected.GetComponent<UnityEngine.UI.Button>() != null)
            {
                if (menuAudioSource != null && moveSelectionSound != null)
                {
                    menuAudioSource.PlayOneShot(moveSelectionSound);
                }
            }
            lastSelectedObject = currentSelected;
        }

        if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            if (menuAudioSource != null && selectConfirmSound != null)
            {
                menuAudioSource.PlayOneShot(selectConfirmSound);
            }

            if (currentSelected != null)
            {
                if (currentSelected.name == "Start Game" || currentSelected.name == "Start") 
                    ClickStartGame();
                else if (currentSelected.name == "Controls" || currentSelected.name == "ControlsMenu") 
                    ClickControls();
                else if (currentSelected.name == "Endless Mode" || currentSelected.name == "EndlessMode") 
                    ClickEndlessMode();
                else if (currentSelected.name == "Quit" || currentSelected.name == "Quit Game") 
                    QuitGame();
            }
        }
    }

    // 🎯 SCENE LOADING ACTIONS (The missing functions that make the screens switch!)
    public void ClickStartGame()
    {
        Scene01Events.storyStateCheckpoint = 0;
        SceneManager.LoadScene("Pong Level 1");
    }

    public void ClickControls()
    {
        SceneManager.LoadScene("ControlsMenu");
    }

    public void ClickEndlessMode()
    {
        SceneManager.LoadScene("EndlessMode");
    }

    public void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}


