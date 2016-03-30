using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
 
public class BlueGoal : MonoBehaviour {

     GameObject RTS;
     int RScore;
     bool isSynced = false;
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
          GameObject RScoreOb = GameObject.Find("Red Team Score");

          RScore++;
          RScoreOb.GetComponent<Text>().text = RScore.ToString();
          canScore = false;
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
               GameObject.Find("Red Team Score").GetComponent<Text>().text = RScore.ToString();
               GameObject.Find("Timer").GetComponent<Timer>().timer = (float)PlayerPrefs.GetInt("Time"); 
               isSynced = true;

          }
     }

     //Detect when ball entes goal area
     void OnTriggerExit(Collider obj)
     {
          if (obj.gameObject.name == "Ball")
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
          //Check if we already have the value in playerprefs
          if (!PlayerPrefs.HasKey("RedScore"))
          {
               RScore = 0;
               PlayerPrefs.SetInt("RedScore", 0);
          }
          else
          {
               RScore = PlayerPrefs.GetInt("RedScore");
          }


     }
     //Used for continuing the scores between goals
     void saveScores()
     {
          PlayerPrefs.SetInt("RedScore", RScore);
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
