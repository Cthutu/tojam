using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class uiDialogueComponent : MonoBehaviour {

    public Text textComponent;
    public Image imageComponent;

    public List<string> dialoguePresets;

	// Use this for initialization
	void Start () {
        ShowCompononents(false);
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Alpha0))
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
        }
    }

    void ShowCompononents(bool _show)
    {
        textComponent.enabled = _show;
        imageComponent.enabled = _show;
    }

    void DisplayDialogue(int _index)
    {
        if(_index < 0 || dialoguePresets.Count <= _index)
        {
            Debug.Log("uiDialogueComponent.DisplayDialogue: _index is not in range of presets.");
            return;
        }

        ShowCompononents(true);

        float oldTextHeight = LayoutUtility.GetPreferredHeight(textComponent.rectTransform);
        float oldTextWidth = LayoutUtility.GetPreferredWidth(textComponent.rectTransform);

        textComponent.text = dialoguePresets[_index];
        float newTextHeight = LayoutUtility.GetPreferredHeight(textComponent.rectTransform);
        float newTextWidth = LayoutUtility.GetPreferredWidth(textComponent.rectTransform);

        float heightScaleFactor = newTextHeight / oldTextHeight;
        float widthScaleFactor = newTextWidth / oldTextWidth;

        //imageComponent.rectTransform.localScale.Scale(new Vector3(widthScaleFactor,heightScaleFactor, 0.0f));
        //imageComponent.rectTransform.sizeDelta = new Vector2(imageComponent.rectTransform.rect.width * widthScaleFactor, imageComponent.rectTransform.rect.height * heightScaleFactor);
    }
}
