using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool isPlayer1Goal;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            GameManager0 manager = GameObject.Find("GameManager0").GetComponent<GameManager0>();

            if (!isPlayer1Goal)
            {
                Debug.Log("Player 1 Scored...");
                manager.Player1Scored();
            }
            else
            {
                Debug.Log("Player 2 Scored...");
                manager.Player2Scored();
            }
        }
    }
}
