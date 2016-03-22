using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RedGoal : MonoBehaviour {

     GameObject BTS;
     int BScore;
     void Start()
     {
          //Create ref to the object before deactivating
          BTS = GameObject.Find("BlueTeamScored");
          BTS.SetActive(false);
     }

     bool canScore = true;
     void incrementScore()
     {
          GameObject BScoreOb = GameObject.Find("Blue Team Score");
          
          int b = int.Parse(BScoreOb.GetComponent<Text>().text);
          b++;
          BScoreOb.GetComponent<Text>().text = b.ToString();
          canScore = false;
     }


     // Update is called once per frame
     void Update()
     {
          if (isTimerOn)
          {
               
               afterGoalTimer();
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
     int minutes;
     int seconds;
     bool isTimerOn = false;
     void afterGoalTimer()
     {
          timer -= Time.deltaTime;
          minutes = (int)timer / 60;
          seconds = (int)timer % 60;

          if (timer <= 0)
          {
               SceneManager.LoadScene("MiniGame");
          }
     }
     
}



