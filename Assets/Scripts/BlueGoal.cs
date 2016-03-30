using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BlueGoal : MonoBehaviour {

     GameObject RTS;
     int RScore;
     GameObject RScoreOb = GameObject.Find("Red Team Score");
     GameObject BScoreOb = GameObject.Find("Blue Team Score");
     void Start()
     {
          
          if (!PlayerPrefs.HasKey("BlueScore"))
          {
               Debug.Log("Set Blue");
               PlayerPrefs.SetInt("BlueScore", 0);
          }
          
          if (!PlayerPrefs.HasKey("RedScore"))
          {
               Debug.Log("Set Red");
               PlayerPrefs.SetInt("RedScore", 0);

          }
          RScoreOb.GetComponent<Text>().text = PlayerPrefs.GetInt("RedScore").ToString();
          BScoreOb.GetComponent<Text>().text = PlayerPrefs.GetInt("BlueScore").ToString();

          Debug.Log(PlayerPrefs.GetInt("RedScore"));
          Debug.Log(PlayerPrefs.GetInt("BlueScore"));

          //Create ref to the object before deactivating
          RTS = GameObject.Find("RedTeamScored");
          RTS.SetActive(false);
     }
     bool canScore = true;
     void incrementScore()
     {
          SceneManager.LoadScene("MiniGame");
          int RScore = PlayerPrefs.GetInt("RedScore");
          RScore++;
          PlayerPrefs.SetInt("RedScore", RScore);
          RScoreOb.GetComponent<Text>().text = RScore.ToString();
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


          if (obj.gameObject.name == "Ball")
          {
               Debug.Log("Red Team Scored!");
               incrementScore();
               SceneManager.LoadScene("MiniGame");
               RTS.SetActive(true);
             
               //isTimerOn = true;
               
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
