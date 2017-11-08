using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    // Static instance of this manager (singleton)
    public static DialogueManager Instance;

    // Canvas that holds the dialogue elements
    public Canvas dialogueCanvas;

    // Text that will be inside the dialog box.
    public Text dialogueText;

    // Reference to player 
    public PlayerController pController;

    // Speed at which characters are displayed for dialogue
    public float playSpeed;

    // Text conversation to play
    private List<Dialogue.DialogueStatement> currentConversation;

    private int statementIndex = 0;
    private bool isPlaying = false;
    private bool goingToPlay = false;

	// Use this for initialization
	void Start () {
        // Make sure there is only one instance 
		if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        // Disable the Canvas on start
        dialogueCanvas.enabled = false;
	}

    public void SetDialogSequence(List<Dialogue.DialogueStatement> sequence)
    {
        statementIndex = 0;
        currentConversation = sequence;
    }

    public void ActivateDialog()
    {
        dialogueCanvas.enabled = true;
        //isPlaying = true;
        goingToPlay = true;
        statementIndex = 0;
        dialogueText.text = currentConversation[0].statement;

        // disable player controls while dialogue is happening
        pController.enabled = false;
    }

	// Update is called once per frame
	void Update () {

		if(isPlaying && Input.GetButtonDown("Interact"))
        {
            statementIndex++;

            // Did we reach the end of the dialogue?
            if(statementIndex >= currentConversation.Count)
            {
                isPlaying = false;
                goingToPlay = false;
                dialogueCanvas.enabled = false;
                pController.enabled = true;
            }
            else
            {
                dialogueText.text = currentConversation[statementIndex].statement;
            }
        }
        else if (goingToPlay && !isPlaying)
        {
            isPlaying = true;
        }
	}

}
