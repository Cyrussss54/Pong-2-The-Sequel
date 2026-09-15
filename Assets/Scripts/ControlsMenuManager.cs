using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ControlsMenuManager : MonoBehaviour
{
    public GameObject backToMenuButton;

    void Start()
    {
        // Keep the cursor hidden but unlocked for EventSystem focus
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;

        // Automatically highlight the back button on scene load
        if (backToMenuButton != null)
        {
            EventSystem.current.SetSelectedGameObject(backToMenuButton);
        }
    }

    void Update()
    {
        // Failsafe keyboard trigger: Pressing Enter instantly loads the main menu!
        if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            LoadMainMenu();
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
