using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    private GameObject[] menuButtons;
    private int currentSelectionIndex = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;

        // 1. Gather all 4 buttons manually in exact vertical order
        menuButtons = new GameObject[4];
        menuButtons[0] = GameObject.Find("Start Game");
        if (menuButtons[0] == null) menuButtons[0] = GameObject.Find("Start");
        
        menuButtons[1] = GameObject.Find("Controls");
        if (menuButtons[1] == null) menuButtons[1] = GameObject.Find("ControlsMenu");

        menuButtons[2] = GameObject.Find("Endless Mode");
        menuButtons[3] = GameObject.Find("Quit");

        // 2. Highlight the first button on load
        HighlightSelectedButton();
    }

    void Update()
    {
        // 🛠️ FAILSAFE KEYBOARD INJECTION: Completely bypasses the broken Project Settings menu!
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentSelectionIndex = (currentSelectionIndex + 1) % 4; // Move Down
            HighlightSelectedButton();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentSelectionIndex = (currentSelectionIndex - 1 + 4) % 4; // Move Up
            HighlightSelectedButton();
        }
        else if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            // Trigger the correct button action on Enter release!
            if (currentSelectionIndex == 0) ClickStartGame();
            if (currentSelectionIndex == 1) ClickClickControls();
            if (currentSelectionIndex == 2) ClickEndlessMode();
            if (currentSelectionIndex == 3) QuitGame();
        }
    }

    private void HighlightSelectedButton()
    {
        if (menuButtons != null && menuButtons[currentSelectionIndex] != null)
        {
            EventSystem.current.SetSelectedGameObject(menuButtons[currentSelectionIndex]);
        }
    }

    public void ClickStartGame()
    {
        Scene01Events.storyStateCheckpoint = 0;
        SceneManager.LoadScene("Pong Level 1");
    }

    public void ClickClickControls()
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
