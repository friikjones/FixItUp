using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject leftHand;
    public GameObject rightRotate;
    public GameObject leftRotate;

    public float rightSpeed;
    public float leftSpeed;
    public bool rightClock;
    public bool leftClock;
    public int rightCounter, rightThreshold;
    public int leftCounter, leftThreshold;


    public float timer;
    public int countdown;
    public Text timerText;
    public Text scoreText;
    public bool gameOver;
    public float score;
    public float scoreUp;

    public GameObject gameOverOverlay;
    public Text highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        countdown = 20;
        leftThreshold = Random.Range(100, 300);
        rightThreshold = Random.Range(100, 300);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            gameOver = false;
        }

        //Clock stuff

        if (rightClock)
        {
            rightRotate.transform.localScale = new Vector3(30, 30, 60);
        }
        else
        {
            rightRotate.transform.localScale = new Vector3(-30, 30, 60);
        }
        if (leftClock)
        {
            leftRotate.transform.localScale = new Vector3(30, 30, 60);
        }
        else
        {
            leftRotate.transform.localScale = new Vector3(-30, 30, 60);
        }

        //Gameplay Stuff
        if (!gameOver)
        {
            rightCounter++;
            if (rightCounter > rightThreshold)
            {
                rightCounter = 0;
                rightThreshold = Random.Range(100, 300);
                rightClock = !rightClock;
            }

            leftCounter++;
            if (leftCounter > leftThreshold)
            {
                leftCounter = 0;
                leftThreshold = Random.Range(100, 300);
                leftClock = !leftClock;
            }

            if (Input.GetKey(KeyCode.L))
            {
                rightHand.transform.Rotate(0, 0, rightSpeed);
                if (!rightClock)
                {
                    score += scoreUp;
                }
            }
            if (Input.GetKey(KeyCode.J))
            {
                rightHand.transform.Rotate(0, 0, -rightSpeed);
                if (rightClock)
                {
                    score += scoreUp;
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                leftHand.transform.Rotate(0, 0, -leftSpeed);
                if (leftClock)
                {
                    score += scoreUp;
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                leftHand.transform.Rotate(0, 0, leftSpeed);
                if (!leftClock)
                {
                    score += scoreUp;
                }
            }
        }
        //Timer stuff
        if (!gameOver)
        {
            timer += Time.deltaTime;
            countdown = Mathf.RoundToInt(20 - timer);
            gameOverOverlay.SetActive(false);
        }
        if (countdown == 0)
        {
            gameOver = true;
            timer = 0;
            if(score > PlayerPrefs.GetInt("Highscore")){
            PlayerPrefs.SetInt("Highscore",Mathf.RoundToInt(score));
            }
            score = 0;
            gameOverOverlay.SetActive(true);
            highScoreText.text = PlayerPrefs.GetInt("Highscore").ToString();
        }

        //Show on screen
        timerText.text = countdown.ToString();
        scoreText.text = score.ToString();
    }
}
