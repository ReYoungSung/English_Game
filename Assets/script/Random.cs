using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random : MonoBehaviour

{

    public float Speed = 50.0f;

    private Transform myTransform = null;



    // Start is called before the first frame update

    void Start()

    {

        myTransform = GetComponent<Transform>();

    }



    // Update is called once per frame

    void Update()

    {

        Vector3 moveAmount = Speed * Vector3.back * Time.deltaTime;

        myTransform.Translate(moveAmount);



        if (myTransform.position.y <= -60.0f)

        {

            myTransform.position = new Vector3(UnityEngine.Random.Range(-60.0f, 60.0f), 60.0f, 0.0f);

        }

    }

}
