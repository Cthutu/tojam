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
            if(time < 0)
            {
                timeStarted = false;
                // fire times up event
            }
        }
	}

    void SetLevelTime(float _time)
    {
        time = _time;
    }

    void StartTimer(bool _start = true)
    {
        timeStarted = _start;
    }
}
