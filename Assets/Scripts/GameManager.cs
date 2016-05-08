using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    [SerializeField]
    public GridRenderer grid;

    [SerializeField]
    public GameUIController uiController;

    [SerializeField]
    public PlayerControl player;

    public int playersCurrentLevel;
    public bool levelLoaded = false;

    private static GameManager instance = null;
    public static GameManager GetInstance() { return instance; }

	// Use this for initialization
	void Start () {
        if (grid == null)
        {
            Debug.LogError("GRID IS NULL");
        }
        if (uiController == null)
        {
            Debug.LogError("UI CONTROLLER IS NULL");
        }
        if (player == null)
        {
            Debug.LogError("PLAYER IS NULL");
        }

        if (instance == null)
        {
            instance = this;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartLevel()
    {
        grid.HandleLoadLevel(playersCurrentLevel);
        levelLoaded = true;
        uiController.EnableGameUi();
        uiController.StartDialogue(0);
    }

    public void TimerFinished()
    {
        uiController.StartDialogue(3);
    }

    public void PlayerDied()
    {
        // trigger player animation

        uiController.StartDialogue(3);
    }

    public void UpdatePlayerLives(int _currentLives)
    {
        uiController.SetPlayerLives(_currentLives);
    }

    public void MissionSuccess()
    {
        uiController.StartDialogue(2);
    }

    public void FirstMonsterAttack()
    {

    }

    public void DialogFinished(int _dialogueIndex)
    {
        switch(_dialogueIndex)
        {
            case 0:
                {
                    // Intro Dialogue
                    uiController.StartDialogue(1);
                    break;
                }
            case 1:
                {
                    // Game Start Dialogue
                    uiController.StartTimer();
                    player.EnablePlayerInput(true);
                    break;
                }
            case 2:
                {
                    // Player Success Dialogue
                    uiController.EnableGameMenu();
                    grid.HandleUnloadLevel();
                    player.EnablePlayerInput(false);
                    break;
                }
            case 3:
                {
                    // Player Failed Dialogue
                    uiController.EnableGameMenu();
                    grid.HandleUnloadLevel();
                    player.EnablePlayerInput(false);
                    break;
                }
            case 4:
                {
                    // Monsters Attack Dialogue
                    break;
                }
            default:
                {
                    break;
                }
        };
    }
}
