using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene01Events : MonoBehaviour
{

    public GameObject fadeScreenIn;
    public GameObject charBall;
    public GameObject charPaddle2;
    public GameObject textBox;

    void Start()
    {
        StartCoroutine(EventStarter());
    }

    IEnumerator EventStarter()
    {
        yield return new WaitForSeconds(2);
        fadeScreenIn.SetActive(false);
        charBall.SetActive(true);
        yield return new WaitForSeconds(2);
        // this is where our text function will go in future tutorial
        textBox.SetActive(true);
        yield return new WaitForSeconds(2);
        charPaddle2.SetActive(true);
    }


}

