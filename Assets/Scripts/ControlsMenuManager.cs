using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ControlsMenuManager : MonoBehaviour
{
    public GameObject backToMenuButton;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;

        if (backToMenuButton != null)
        {
            EventSystem.current.SetSelectedGameObject(backToMenuButton);
        }
    }

    void Update()
    {
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
