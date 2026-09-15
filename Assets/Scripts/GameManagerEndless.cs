using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerEndless : MonoBehaviour
{
    public GameObject ball;
    public GameObject player1Paddle;
    public TMPro.TextMeshProUGUI scoreTextDisplay; // Drag your unified high-score text layer here!

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
        // Failsafe exit: Tap Escape key to return right back to the Main Menu cleanly
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main Menu");
        }
    }

    // 🎯 WALL HIT TRIGGER HOOK: Automatically adds points every time the ball richochets off the wall!
    public void RegisterWallBounceHit()
    {
        currentPaddleHitsScore++;
        UpdateScoreboardInterface();
    }

    // ❌ BALL DROP HOOK: Resets the entire run score back to 0 when you miss!
    public void RegisterBallDroppedMiss()
    {
        currentPaddleHitsScore = 0;
        UpdateScoreboardInterface();
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
