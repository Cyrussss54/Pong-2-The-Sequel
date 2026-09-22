using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerEndless : MonoBehaviour
{
    public GameObject ball;
    public GameObject player1Paddle;
    public TMPro.TextMeshProUGUI scoreTextDisplay; 

    private int currentPaddleHitsScore = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        
        currentPaddleHitsScore = 0;
        UpdateScoreboardInterface();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main Menu");
        }
    }

    public void RegisterWallBounceHit()
    {
        currentPaddleHitsScore++;
        UpdateScoreboardInterface();
    }

    // ❌ FIX: Forcefully clears out the score back to 0 and instantly refreshes your UI text!
    public void RegisterBallDroppedMiss()
    {
        currentPaddleHitsScore = 0;
        UpdateScoreboardInterface(); // This forces the screen text to immediately update to "0"
        ResetPositions();
    }

    private void UpdateScoreboardInterface()
    {
        if (scoreTextDisplay != null)
        {
            scoreTextDisplay.text = currentPaddleHitsScore.ToString();
        }
    }

    private void ResetPositions()
    {
        if (ball != null && ball.GetComponent<Ball>() != null) ball.GetComponent<Ball>().Reset();
        if (player1Paddle != null && player1Paddle.GetComponent<Paddle>() != null) player1Paddle.GetComponent<Paddle>().Reset();
    }
}
