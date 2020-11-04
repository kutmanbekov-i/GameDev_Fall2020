using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public GameObject paddleObject;
    public float paddleSpeed = 5;

    void Start()
    {

    }

    void FixedUpdate()
    {
        GameObject ball = GameObject.FindWithTag("Ball");
        float x = paddleObject.transform.position.x;

        if (ball.transform.position.z < 0)
        {
            if (ball.transform.position.x > paddleObject.transform.position.x && x < 19.5f)
            {
                x += 0.25f;
            } else
            {
                x -= 0.25f;
            }
            paddleObject.transform.position = new Vector3( x, 0f, -19.5f);
        }
    }

}
