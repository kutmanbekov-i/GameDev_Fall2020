using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    
    private Rigidbody rb;
    
    public float maxSpeed = 100f;

    public AudioClip PongSound;
	private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        audioSource = GetComponent<AudioSource>();
        // float angle = Random.Range(-40.0f, 40.0f);
        // Vector3 force =
        //                 Quaternion.Euler (0.0f, angle, 0.0f) * Vector3.forward *
        //                     ForceScale;

        //         rb.AddForce (force);
    }

    void FixedUpdate()
    {
        // TODO
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

    }
    void OnCollisionEnter(Collision collision)
    {
        audioSource.PlayOneShot(PongSound);
    }
}
