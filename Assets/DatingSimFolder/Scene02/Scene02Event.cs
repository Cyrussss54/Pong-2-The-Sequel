using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene02Event : MonoBehaviour
{

    [SerializeField] GameObject fadeScreenIn;


    void Start()
    {
        StartCoroutine(EventStarter());
    }



    void Update()
    {
        
    }

    IEnumerator EventStarter()
    {
        // event 0
        yield return new WaitForSeconds(2);
        fadeScreenIn.SetActive(true);



    }
}
