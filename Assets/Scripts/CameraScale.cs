using UnityEngine;
using System.Collections;

public class CameraScale : MonoBehaviour {

	void Awake()
	{
		GetComponent<Camera>().orthographicSize = Screen.height / 2.0f;
	}

}
