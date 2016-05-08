using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class uiGameEndOverlayController : MonoBehaviour {

    public Image imageComponent;
    public float fadeTime;

    private bool doFadeIn = false;
    private float startTime;

	// Use this for initialization
	void Start () {
        imageComponent.canvasRenderer.SetAlpha(0.0f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EngageOverlay();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ResetOverlay();
        }
        if (doFadeIn)
        {
            float currentAlpha = imageComponent.canvasRenderer.GetAlpha();
            float lerpValue = (Time.time - startTime) / fadeTime;
            imageComponent.canvasRenderer.SetAlpha(lerpValue);
            if(imageComponent.canvasRenderer.GetAlpha() >= 1.0f)
            {
                imageComponent.canvasRenderer.SetAlpha(1.0f);
                doFadeIn = false;
            }
        }
	}

    public void ResetOverlay()
    {
        imageComponent.canvasRenderer.SetAlpha(0.0f);
        doFadeIn = false;
    }

    public void EngageOverlay()
    {
        doFadeIn = true;
        startTime = Time.time;
    }
}
