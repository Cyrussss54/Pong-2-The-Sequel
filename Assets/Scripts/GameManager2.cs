using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    public GameObject ball;

    [Header("Player 1")]
    public GameObject Player1Paddle;
    public GameObject Player1Goal;

    [Header("Player 2")]
    public GameObject Player2Paddle;
    public GameObject Player2Goal;

    [Header("Score UI Layout links")]
    public GameObject Player1Text;
    public GameObject Player2Text;

    public int Player1Score;
    public int Player2Score;

    public void Player1Scored()
    {
        Player2Score++; 
        if (Player2Text != null && Player2Text.GetComponent<TMPro.TextMeshProUGUI>() != null)
        {
            Player2Text.GetComponent<TMPro.TextMeshProUGUI>().text = Player2Score.ToString();
        }
        ResetPosition();
        CheckMatchEnd();
    }

    public void Player2Scored()
    {
        Player1Score++; 
        if (Player1Text != null && Player1Text.GetComponent<TMPro.TextMeshProUGUI>() != null)
        {
            Player1Text.GetComponent<TMPro.TextMeshProUGUI>().text = Player1Score.ToString();
        }
        ResetPosition();
        CheckMatchEnd();
    }

    private void CheckMatchEnd()
    {
        if (Player1Score >= 5)
        {
            Player1Score = 0;
            Player2Score = 0;
            SceneManager.LoadScene("Pong Level 3");
            return;
        }

        if (Player2Score >= 5)
        {
            Player1Score = 0;
            Player2Score = 0;
            
            if (Player1Text != null && Player1Text.GetComponent<TMPro.TextMeshProUGUI>() != null)
                Player1Text.GetComponent<TMPro.TextMeshProUGUI>().text = "0";
                
            if (Player2Text != null && Player2Text.GetComponent<TMPro.TextMeshProUGUI>() != null)
                Player2Text.GetComponent<TMPro.TextMeshProUGUI>().text = "0";

            ResetPosition();
            Debug.Log("Flipped roles check: AI won, score reset.");
        }
    }

    private void ResetPosition()
    {
        if (ball != null && ball.GetComponent<Ball>() != null) ball.GetComponent<Ball>().Reset();
        if (Player1Paddle != null && Player1Paddle.GetComponent<Paddle>() != null) Player1Paddle.GetComponent<Paddle>().Reset();
        if (Player2Paddle != null && Player2Paddle.GetComponent<Paddle>() != null) Player2Paddle.GetComponent<Paddle>().Reset();
    }
}
