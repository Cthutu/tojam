using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class uiDialogueComponent : MonoBehaviour {

    public Text textComponent;
	public Text shadowComponent;
    public Image imageComponent;

    public List<string> dialoguePresets;

    private int currentDialogueIndex;
    private bool shouldShowDialogue = false;

	// Use this for initialization
	void Start () {
        ShowComponents(shouldShowDialogue);
	}
	
	// Update is called once per frame
	void Update () {
	    /*if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            DisplayDialogue(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DisplayDialogue(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DisplayDialogue(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            DisplayDialogue(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            DisplayDialogue(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            DisplayDialogue(5);
        }*/

        if(shouldShowDialogue && !textComponent.gameObject.activeSelf)
        {
            ShowComponents(true);
        }

        if(textComponent.gameObject.activeSelf && Input.anyKeyDown)
        {
            ShowComponents(false);
			GameObject.Find("Grid").GetComponent<GridRenderer>().ClearRect();

            GameManager gMan = GameManager.GetInstance();
            if(gMan)
            {
                gMan.DialogFinished(currentDialogueIndex);
            }
        }
    }

    void ShowComponents(bool _show)
    {
        textComponent.gameObject.SetActive(_show);
		shadowComponent.gameObject.SetActive(_show);
        //imageComponent.gameObject.SetActive(_show);
        shouldShowDialogue = _show;
    }

    public void DisplayDialogue(int _index)
    {
        if(_index < 0 || dialoguePresets.Count <= _index)
        {
            Debug.Log("uiDialogueComponent.DisplayDialogue: _index is not in range of presets.");
            return;
        }

        currentDialogueIndex = _index;
        ShowComponents(true);

		// Extract text and text rect coords
		string[] texts = dialoguePresets[_index].Split('/');
		string coords = texts[0].TrimStart(new char[]{'('}).TrimEnd(new char[]{')'});
		string[] values = coords.Split(',');

		GameObject.Find("Grid").GetComponent<GridRenderer>().TextRect(
			System.Int32.Parse(values[0]),
			System.Int32.Parse(values[1]),
			System.Int32.Parse(values[2]),
			System.Int32.Parse(values[3]));

		textComponent.text = texts[1];
		shadowComponent.text = texts[1];

        //imageComponent.rectTransform.localScale.Scale(new Vector3(widthScaleFactor,heightScaleFactor, 0.0f));
        //imageComponent.rectTransform.sizeDelta = new Vector2(imageComponent.rectTransform.rect.width * widthScaleFactor, imageComponent.rectTransform.rect.height * heightScaleFactor);
    }
}
