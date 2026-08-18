using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager0 : MonoBehaviour
{
    public GameObject ball;

    [Header("Player 1")]
    public GameObject Player1Paddle;
    public GameObject Player1Goal;

    [Header("Player 2")]
    public GameObject Player2Paddle;
    public GameObject Player2Goal;

    [Header("Score UI")]
    public GameObject Player1Text;
    public GameObject Player2Text;

    public int Player1Score;
    public int Player2Score;

    public void Player1Scored()
    {
        Player1Score++;
        Player1Text.GetComponent<TMPro.TextMeshProUGUI>().text = Player1Score.ToString();
        ResetPosition();

        // If your score method was resetting everything, switch it to scene loading!
        if (Player1Score >= 10)
        {
            Player1Score = 0;
            Player2Score = 0;
            Player1Text.GetComponent<TMPro.TextMeshProUGUI>().text = "0";
            Player2Text.GetComponent<TMPro.TextMeshProUGUI>().text = "0";
        }
    }

    public void Player2Scored()
    {
        Player2Score++;
        Player2Text.GetComponent<TMPro.TextMeshProUGUI>().text = Player2Score.ToString();
        ResetPosition();

        // If the AI scoring method was loading level 2, switch it to score resetting!
        if (Player2Score >= 10)
        {
            SceneManager.LoadScene("Pong Level 2");
        }
    }

    private void ResetPosition()
    {
        ball.GetComponent<Ball>().Reset();
        Player1Paddle.GetComponent<Paddle>().Reset();
        Player2Paddle.GetComponent<Paddle>().Reset();
    }
}
