using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class uiGameTimerController : MonoBehaviour {

    public Text textComponent;

    public float time;
    private bool timeStarted;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(timeStarted)
        {
            time -= Time.deltaTime;

            textComponent.text = FormatTimeString();
            if (time < 0)
            {
                timeStarted = false;
                GameManager gMan = GameManager.GetInstance();
                if(gMan)
                {
                    gMan.TimerFinished();
                }
            }
        }
	}

    string FormatTimeString()
    {
        float timeInSeconds = time;
        float timeInMinutes = timeInSeconds / 60;
        timeInSeconds = timeInSeconds % 60;
        float timeInHours = timeInMinutes / 60;
        timeInMinutes = timeInMinutes % 60;
        float timeInDays = timeInHours / 24;
        timeInHours = timeInHours % 24;

        return ((int)timeInDays).ToString() + ":" + ((int)timeInHours).ToString() + ":" + ((int)timeInMinutes).ToString() + ":" + ((int)timeInSeconds).ToString();
    }

    public void SetLevelTime(float _time)
    {
        time = _time;
    }

    public void StartTimer(bool _start = true)
    {
        timeStarted = _start;
    }
}
