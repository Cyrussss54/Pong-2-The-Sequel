using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerMini : MonoBehaviour
{
    public GameObject ball;

    [Header("Player 1")]
    public GameObject Player1Paddle;
    public GameObject Player1Goal;

    [Header("Player 2 (AI)")]
    public GameObject Player2Paddle;
    public GameObject Player2Goal;

    [Header("Score UI")]
    public GameObject Player1Text;
    public GameObject Player2Text;

    public int Player1Score;
    public int Player2Score;

    private string displayMessage = "";
    private bool showBox = false; 
    private bool gameHasStarted = false; 
    private bool isTypingComplete = false;

    public static bool playerWonMinigame = false;

    void Start()
    {
        showBox = true;
        gameHasStarted = false;
        isTypingComplete = false;

        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
        if (ballRb != null)
        {
            ballRb.linearVelocity = Vector2.zero;
            ballRb.bodyType = RigidbodyType2D.Kinematic; 
        }

        StartCoroutine(TypeTextRoutine());
    }

    IEnumerator TypeTextRoutine()
    {
        displayMessage = "";
        foreach (char letter in "We will battle for Ballarina, first to 3 points WIN.")
        {
            displayMessage += letter;
            yield return new WaitForSeconds(0.04f); 
        }
        isTypingComplete = true;
    }

    void Update()
    {
        if (showBox && !gameHasStarted)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                if (!isTypingComplete)
                {
                    StopAllCoroutines();
                    displayMessage = "We will battle for Ballarina, first to 3 points WIN.";
                    isTypingComplete = true;
                }
                else
                {
                    StartCoroutine(DismissIntroAndStartMatch());
                }
            }
        }
    }

    IEnumerator DismissIntroAndStartMatch()
    {
        gameHasStarted = true;
        displayMessage = ""; 
        showBox = false;

        yield return new WaitForEndOfFrame();

        if (ball != null) ball.transform.position = Vector3.zero;

        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
        if (ballRb != null)
        {
            ballRb.bodyType = RigidbodyType2D.Dynamic;
        }

        ResetPosition();
    }

    public void Player1Scored()
    {
        Player1Score++;
        UpdateScoreUI();
        ResetPosition();
        
        if (Player1Score >= 3)
        {
            // HARD ROLE REVERSAL: If this trigger hits 3, it actually means you LOST!
            playerWonMinigame = false; 
            EndMatchAndLoadLevel3();
        }
    }

    public void Player2Scored()
    {
        Player2Score++;
        UpdateScoreUI();
        ResetPosition();
        
        if (Player2Score >= 3)
        {
            // HARD ROLE REVERSAL: If this trigger hits 3, it actually means you WON!
            playerWonMinigame = true; 
            EndMatchAndLoadLevel3();
        }
    }

    private void EndMatchAndLoadLevel3()
    {
        Player1Score = 0;
        Player2Score = 0;
        SceneManager.LoadScene("Pong Level 3");
    }

    void OnGUI()
    {
        if (showBox && !string.IsNullOrEmpty(displayMessage))
        {
            float boxWidth = 200f; 
            float boxHeight = 150f; 
            float boxX = 20f; 
            float boxY = Screen.height * 0.25f; 

            GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.normal.background = Texture2D.whiteTexture; 
            GUI.backgroundColor = new Color(0.05f, 0.05f, 0.05f, 0.95f); 
            
            Rect dialogueBoxRect = new Rect(boxX, boxY, boxWidth, boxHeight);
            GUI.Box(dialogueBoxRect, "", boxStyle);

            GUIStyle textStyle = new GUIStyle(GUI.skin.label);
            textStyle.fontSize = 16;                   
            textStyle.fontStyle = FontStyle.Bold;      
            textStyle.alignment = TextAnchor.MiddleCenter;
            textStyle.wordWrap = true;                 

            float padding = 10f;
            Rect labelRect = new Rect(boxX + padding, boxY + padding, boxWidth - (padding * 2), boxHeight - (padding * 2));

            textStyle.normal.textColor = Color.black; 
            GUI.Label(new Rect(labelRect.x + 1, labelRect.y + 1, labelRect.width, labelRect.height), displayMessage, textStyle);

            textStyle.normal.textColor = Color.white; 
            GUI.Label(labelRect, displayMessage, textStyle);
        }
    }

    private void UpdateScoreUI()
    {
        if (Player1Text != null && Player1Text.GetComponent<TMPro.TextMeshProUGUI>() != null)
            Player1Text.GetComponent<TMPro.TextMeshProUGUI>().text = Player1Score.ToString();
        if (Player2Text != null && Player2Text.GetComponent<TMPro.TextMeshProUGUI>() != null)
            Player2Text.GetComponent<TMPro.TextMeshProUGUI>().text = Player2Score.ToString();
    }

    private void ResetPosition()
    {
        if (ball != null && ball.GetComponent<Ball>() != null) ball.GetComponent<Ball>().Reset();
        if (Player1Paddle != null && Player1Paddle.GetComponent<Paddle>() != null) Player1Paddle.GetComponent<Paddle>().Reset();
        if (Player2Paddle != null && Player2Paddle.GetComponent<Paddle>() != null) Player2Paddle.GetComponent<Paddle>().Reset();
    }
}
