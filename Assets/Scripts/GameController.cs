using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public BallController Ballprefab;

    public Text ScoreAText;
    private uint scoreA = 0;
    public Text ScoreBText;
    private uint scoreB = 0;

    public Text TimeText;
    private float timeRemaining;
    private bool timerIsRunning = false;

    public Text infoText;
    static bool pauseEnabled = false;


    public float BallForceScale = 10.0f;

    void Start()
    {
        Time.timeScale = 1;
        timeRemaining = 120;
        timerIsRunning = true;

        infoText.text = "";
        scoreA = 0;
        scoreB = 0;
        ScoreAText.text = scoreA.ToString();
        ScoreBText.text = scoreB.ToString();

        Destroy(GameObject.FindWithTag("Ball"));
        SpawnBall(Vector3.forward);

        GameObject.FindWithTag("PaddleA").transform.position = new Vector3(0f, 0f, 19.5f);
        GameObject.FindWithTag("PaddleB").transform.position = new Vector3(0f, 0f, -19.5f);
    }

    // FixedUpdate is not working with Time.deltatime((
    void Update()
    {
        CheckWin();
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) )
        {    
            pauseEnabled = !pauseEnabled;
            PauseGame();
        }
        
    }

    void PauseGame()
    {
        if (pauseEnabled)
        {
            Time.timeScale = 0f;
            infoText.text = "PAUSED";
        } else
        {
            Time.timeScale = 1;
            infoText.text = "";
        }
    }
    
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        TimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void CheckWin()
    {
        if (timeRemaining == 0)
        {
            Time.timeScale = 0;

            if (scoreA > scoreB)
            {
                infoText.text = "Congratulations!\nYou won!";
            }
            else if (scoreB > scoreA)
            {
                infoText.text = "You lost!\nTry again!";
            }
            else
            {
                infoText.text = "Tie!";
            }
            infoText.text += "\nPress <SPACE> to start new game!";

            if (Input.GetKeyDown(KeyCode.Space))
            {
                // RE-Start, actually
                Start();
            }
            
        } else if (scoreA >= 11 || scoreB >= 11)
        {
            Time.timeScale = 0;

            if (scoreA >= 11)
            {
                infoText.text = "Congratulations!\nYou won!";
            }
            else if (scoreB >= 11)
            {
                infoText.text = "You lost!\nTry again!";
            }
            infoText.text += "\nPress <SPACE> to start new game!";

            if (Input.GetKeyDown(KeyCode.Space))
            {
                // RE-Start, actually
                Start();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {                           // delay
        Destroy(other.gameObject, 0); // the ball is the only object that can leave the area
         
        if (other.transform.position.z > transform.position.z)
        {
            SpawnBall(Vector3.forward * -1);
            
            scoreB++;
            ScoreBText.text = scoreB.ToString();
        }
        else
        {
            SpawnBall(Vector3.forward);
            scoreA++;
            ScoreAText.text = scoreA.ToString();
        }
    }

    private void SpawnBall(Vector3 dir)
    {
        float angle = Random.Range(-40.0f, 40.0f);
        Vector3 impulse =
                        Quaternion.Euler (0.0f, angle, 0.0f) * dir *
                            BallForceScale;

        BallController ball = Instantiate(Ballprefab);
        ball.GetComponent<Rigidbody>().AddForce (impulse);

    }
}
