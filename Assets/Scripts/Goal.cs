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
            GameManagerEndless endlessManager = Object.FindFirstObjectByType<GameManagerEndless>();
            if (endlessManager != null)
            {
                if (isPlayer1Goal) 
                {
                    endlessManager.RegisterBallDroppedMiss();
                }
                return;
            }

            GameManager0 managerLvl1 = Object.FindFirstObjectByType<GameManager0>();
            if (managerLvl1 != null)
            {
                if (!isPlayer1Goal) managerLvl1.Player1Scored();
                else managerLvl1.Player2Scored();
                return;
            }

            GameManager2 managerLvl2 = Object.FindFirstObjectByType<GameManager2>();
            if (managerLvl2 != null)
            {
                if (!isPlayer1Goal) managerLvl2.Player1Scored();
                else managerLvl2.Player2Scored();
                return;
            }

            GameManagerMini managerMini = Object.FindFirstObjectByType<GameManagerMini>();
            if (managerMini != null)
            {
                if (!isPlayer1Goal) managerMini.Player1Scored();
                else managerMini.Player2Scored();
                return;
            }
        }
    }
}
