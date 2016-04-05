using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon;

public class Timer : Photon.MonoBehaviour
{
     //300 seconds = 5 minutes
     //when adjusting play time you must also adjust the Timer(Script)

     public float timer = 300;
    Text txt;
    int minutes;
    int seconds;

     

     void Update()
    {
        timer -= Time.deltaTime;
        minutes = (int) timer / 60;
        seconds = (int) timer % 60;
        txt.text = minutes.ToString() + ":" + seconds.ToString("00");
        if (timer <= 0)
        {
              
               timer = 0;
               if ((int)PhotonNetwork.masterClient.customProperties["RedScore"] > (int)PhotonNetwork.masterClient.customProperties["BlueScore"])
               {
                    RTW.SetActive(true);
               }
               else
               if ((int)PhotonNetwork.masterClient.customProperties["BlueScore"] > (int)PhotonNetwork.masterClient.customProperties["RedScore"])
               {
                    
                    BTW.SetActive(true);
               }
               else
               {
                    
                    TG.SetActive(true);
               }

               afterGoalTimer();

          }
    }

    //Wait 3 seconds before restarting game
     public float wintimer = 7;

     void afterGoalTimer()
     {
          wintimer -= Time.deltaTime;
          if (wintimer <= 0)
          {
               PhotonNetwork.Disconnect();
               PhotonNetwork.LoadLevel("MainMenu");
          }
     }
     public float GetTime()
	{
		return timer;
	}

     GameObject RTW;
     GameObject BTW;
     GameObject TG;
     void Start()
    {
          RTW = GameObject.Find("RedTeamWins");
          RTW.SetActive(false);
          BTW = GameObject.Find("BlueTeamWins");
          BTW.SetActive(false);
          TG = GameObject.Find("TieGame");
          TG.SetActive(false);

          timer = (float)((int)PhotonNetwork.masterClient.customProperties["Time"]);
          txt = GetComponent<Text>();
        txt.text = "00.00";
    }
}

