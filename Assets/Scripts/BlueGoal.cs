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
     void incrementScore()
     {
          GameObject RScoreOb = GameObject.Find("Red Team Score");
      
          int r = int.Parse(RScoreOb.GetComponent<Text>().text);
          r++;
          RScoreOb.GetComponent<Text>().text = r.ToString();
     }


     // Update is called once per frame
     void Update()
     {

     }

     //Detect when ball entes goal area
     void OnTriggerExit(Collider obj)
     {


          if (obj.gameObject.name == "Ball")
          {
               Debug.Log("Red Team Scored!");
               incrementScore();
               RTS.SetActive(true);

               SceneManager.LoadScene("MiniGame");
          }
     }
}
