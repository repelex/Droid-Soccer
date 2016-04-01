using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class RedGoal : MonoBehaviour
{

    GameObject BTS;
    int BScore;
    bool isSynced = false;

    public AudioClip scoreSound;
    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;


    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        //Create ref to the object before deactivating
        BTS = GameObject.Find("BlueTeamScored");
        BTS.SetActive(false);
        getScores();

    }

    bool canScore = true;
    void incrementScore()
    {
        float vol = UnityEngine.Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(scoreSound, vol);

        GameObject BScoreOb = GameObject.Find("Blue Team Score");

        BScore++;
        BScoreOb.GetComponent<Text>().text = BScore.ToString();
        canScore = false;
        saveScores();
    }


    // Update is called once per frame
    void Update()
    {
        if (isTimerOn)
        {
            afterGoalTimer();
        }
        if (!isSynced)
        {
            GameObject.Find("Blue Team Score").GetComponent<Text>().text = BScore.ToString();
            GameObject.Find("Timer").GetComponent<Timer>().timer = (float)PlayerPrefs.GetInt("Time");
            isSynced = true;

        }
    }

    //Detect when ball entes goal area
    void OnTriggerExit(Collider obj)
    {
        if (obj.gameObject.name == "Ball" && canScore)
        {
            Debug.Log("Blue Team Scored!");
            incrementScore();
            BTS.SetActive(true);
            isTimerOn = true;
        }
    }

    //Wait 3 seconds before reloading the scene
    public float timer = 4;
    bool isTimerOn = false;
    void afterGoalTimer()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            saveScores();
            saveTime();
            SceneManager.LoadScene("MiniGame");
        }
    }

    //Load scores
    void getScores()
    {
        //Check if we already have the value in playerprefs
        if (!PlayerPrefs.HasKey("BlueScore"))
        {
            BScore = 0;
            PlayerPrefs.SetInt("BlueScore", 0);
        }
        else
        {
            BScore = PlayerPrefs.GetInt("BlueScore");
        }


    }
    //Used for continuing the scores between goals
    void saveScores()
    {
        PlayerPrefs.SetInt("BlueScore", BScore);
    }

    //Saves the current time on the clock
    void saveTime()
    {
        string currTime = GameObject.Find("Timer").GetComponent<Text>().text.ToString();
        string[] times = currTime.Split(':');
        int timeRemaining = (Int32.Parse(times[0]) * 60) + Int32.Parse(times[1]);
        PlayerPrefs.SetInt("Time", timeRemaining);
    }
}



