using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    void Start()
    {
        // 1. Hide the mouse cursor completely
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 2. Automatically highlight the Start Game button on load
        GameObject firstButton = GameObject.Find("Start Game"); 
        if (firstButton != null)
        {
            EventSystem.current.SetSelectedGameObject(firstButton);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
