using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class uiLifeDisplayController : MonoBehaviour {

    public Text textComponent;
    public SpriteRenderer spriteComponent;

    private int m_currentLives;

    //[SerializeField]
    //public PlayerController player;

	// Use this for initialization
	void Start () {
        //player.SetLifeUIElement(this);
	}
	
	// Update is called once per frame
	void Update () {
        //UpdateLife((int)(Time.time));
	}

    public void UpdateLife(int currentLives)
    {
        m_currentLives = currentLives;

        textComponent.text = "x" + m_currentLives.ToString();
    }
}
