using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BlueGoal : MonoBehaviour {

     GameObject RTS;
     int RScore;
     void Start()
     {
          
          //Create ref to the object before deactivating
          RTS = GameObject.Find("RedTeamScored");
          RTS.SetActive(false);
     }
     bool canScore = true;
     void incrementScore()
     {
          GameObject RScoreOb = GameObject.Find("Red Team Score");
      
          int r = int.Parse(RScoreOb.GetComponent<Text>().text);
          r++;
          RScoreOb.GetComponent<Text>().text = r.ToString();
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
               RTS.SetActive(true);
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
