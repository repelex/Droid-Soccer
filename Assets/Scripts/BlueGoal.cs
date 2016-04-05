using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class BlueGoal : MonoBehaviour
{

    GameObject RTS;
    int RScore;
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
        RTS = GameObject.Find("RedTeamScored");
        RTS.SetActive(false);
        getScores();
    }

    bool canScore = true;

    void incrementScore()
    {
          canScore = false;
          float vol = UnityEngine.Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(scoreSound, vol);

        GameObject RScoreOb = GameObject.Find("Red Team Score");
        RScore++;
        PhotonNetwork.masterClient.customProperties["RedScore"] = RScore;
        RScoreOb.GetComponent<Text>().text = RScore.ToString();
        
        saveScores();

    }


    // Update is called once per frame
    void Update()
    {
        saveTime();
        if (isTimerOn)
        {
            afterGoalTimer();
        }
        if (!isSynced)
        {
            
            GameObject.Find("Red Team Score").GetComponent<Text>().text = ((int)PhotonNetwork.masterClient.customProperties["RedScore"]).ToString();
            GameObject.Find("Timer").GetComponent<Timer>().timer = (float)PlayerPrefs.GetInt("Time");
            isSynced = true;

        }
    }

    //Detect when ball entes goal area
    void OnTriggerExit(Collider obj)
    {
        if (obj.gameObject.name == "Ball" && canScore)
        {
            Debug.Log("Red Team Scored!");
            incrementScore();
            RTS.SetActive(true);
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
            saveTime();
            saveScores();

            SceneManager.LoadScene("MiniGame");

        }
    }

    //Load scores
    void getScores()
    {
          RScore = (int)PhotonNetwork.masterClient.customProperties["RedScore"];
          PlayerPrefs.SetInt("RedScore", (int)PhotonNetwork.masterClient.customProperties["RedScore"]);
          PlayerPrefs.SetInt("BlueScore", (int)PhotonNetwork.masterClient.customProperties["BlueScore"]);

     }
    //Used for continuing the scores between goals
    void saveScores()
    {
        PlayerPrefs.SetInt("RedScore", (int)PhotonNetwork.masterClient.customProperties["RedScore"]);
    }

    //Saves the current time on the clock
    void saveTime()
    {
        string currTime = GameObject.Find("Timer").GetComponent<Text>().text.ToString();
        string[] times = currTime.Split(':');
        int timeRemaining = (Int32.Parse(times[0]) * 60) + Int32.Parse(times[1]);
          PhotonNetwork.masterClient.customProperties["Time"] = timeRemaining;
        PlayerPrefs.SetInt("Time", (int)PhotonNetwork.masterClient.customProperties["Time"]);
    }


}
