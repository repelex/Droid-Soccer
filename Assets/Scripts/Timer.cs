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
            SceneManager.LoadScene("Lobby");
            
        }
    }
	public float GetTime()
	{
		return timer;
	}

    void Start()
    {
        txt = GetComponent<Text>();
        txt.text = "00.00";
    }
}

