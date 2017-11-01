using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    // Static instance of this manager (singleton)
    public static DialogueManager instance;

    // Text that will be inside the dialog box.
    public Text dialogText;

    // Speed at which characters are displayed for dialogue
    public float playSpeed;

    // Text conversation to play
    private List<Dialogue.DialogueStatement> currentConversation;

    // Is the manager currently showing text?
    private bool isPlayingText = false;

    // Is the manager done revealing text?
    private bool finishedPlaying = false;

	// Use this for initialization
	void Start () {


        // Make sure there is only one instance 
		if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
	}

    public void SetDialogSequence(List<Dialogue.DialogueStatement> sequence)
    {
        currentConversation = sequence;
    }

    public void ActivateDialog()
    {
        dialogText.text = currentConversation[0].statement;
        // enable the dialog panel/canvas
        // start displaying dialog (coroutine?)
    }

	// Update is called once per frame
	void Update () {
		
	}

}
