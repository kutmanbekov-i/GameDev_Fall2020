using UnityEngine;

public class PaddleController : MonoBehaviour {

        public float InputForceScale = 100.0f;

        public float ForceAppliedToBallScale = 4.0f;

        private Rigidbody rb;

        void Start ()
	{
                rb = GetComponent<Rigidbody> ();
        }

        void FixedUpdate()
        {
                float hAxis = - Input.GetAxis("Horizontal");

                hAxis *= InputForceScale;

                rb.AddForce(new Vector3(hAxis, 0.0f, 0.0f));
        }

        void OnCollisionEnter(Collision collision)
        {
                GameObject gameObject =
                        collision.gameObject;

                if (gameObject.CompareTag ("Ball")) {
                        GameObject ball =
                                gameObject;

                        float shift =
                                ball.transform.position.x -
                                        transform.position.x;
                                        
                        shift *= ForceAppliedToBallScale;

                        Vector3 force =
                                new Vector3 (shift, 0.0f, 0.0f);

                        ball.GetComponent<Rigidbody> ().AddForce (force);
                }
        }

}