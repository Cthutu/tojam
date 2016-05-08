using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class GameUIController : MonoBehaviour {

    public List<GameObject> gameUiComponents;
    public List<GameObject> gameMenuComponents;

    public float gameTimer = 300.0f; // in seconds

    private AudioSource music;

	// Use this for initialization
	void Start () {
        music = gameObject.AddComponent<AudioSource>();
        music.outputAudioMixerGroup = Resources.Load<AudioMixer>("Main").FindMatchingGroups("BGM")[0];
        music.clip = Resources.Load<AudioClip>("BGM/Intro");
        music.loop = false;
        music.Play();
        EnableGameMenu();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void EnableGameMenu()
    {
        foreach (GameObject ui in gameUiComponents)
        {
            ui.SetActive(false);
        }

        foreach (GameObject ui in gameMenuComponents)
        {
            ui.SetActive(true);
        }
    }

    public void EnableGameUi()
    {
        music.clip = Resources.Load<AudioClip>("BGM/Gameplay");
        music.loop = true;
        music.Play();

        foreach (GameObject ui in gameUiComponents)
        {
            ui.SetActive(true);
        }

        foreach (GameObject ui in gameMenuComponents)
        {
            ui.SetActive(false);
        }
    }

    public void StartDialogue(int _index)
    {
        foreach(GameObject ui in gameUiComponents)
        {
            uiDialogueComponent dialogueComponent = ui.GetComponent<uiDialogueComponent>();
            if(dialogueComponent)
            {
                dialogueComponent.DisplayDialogue(_index);
            }
        }
    }

    public void StartTimer()
    {
        foreach (GameObject ui in gameUiComponents)
        {
            uiGameTimerController gameTimeController = ui.GetComponent<uiGameTimerController>();
            if (gameTimeController)
            {
                gameTimeController.SetLevelTime(gameTimer);
                gameTimeController.StartTimer();
            }
        }
    }

    public void StopTimer()
    {
        foreach (GameObject ui in gameUiComponents)
        {
            uiGameTimerController gameTimeController = ui.GetComponent<uiGameTimerController>();
            if (gameTimeController)
            {
                gameTimeController.StartTimer(false);
            }
        }
    }

    public void SetPlayerLives(int _currentLives)
    {
        foreach (GameObject ui in gameUiComponents)
        {
            uiLifeDisplayController lifeDisplayController = ui.GetComponent<uiLifeDisplayController>();
            if (lifeDisplayController)
            {
                lifeDisplayController.UpdateLife(_currentLives);
            }
        }
    }

    public void StartEndGameOverlay(bool _engage = true)
    {
        foreach (GameObject ui in gameUiComponents)
        {
            uiGameEndOverlayController endOverlayController = ui.GetComponent<uiGameEndOverlayController>();
            if (endOverlayController)
            {
                if (_engage)
                {
                    endOverlayController.EngageOverlay();
                }
                else
                {
                    endOverlayController.ResetOverlay();
                }
            }
        }
    }
}
