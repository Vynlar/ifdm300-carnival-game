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

    public GameObject buttonPrefab;

    // The buttons for each dialogue action
    private List<Button> dialogueActionsText;

    // Reference to player 
    public PlayerController pController;

    // Speed at which characters are displayed for dialogue
    public float playSpeed;

    // Text conversation to play
    private Dialogue.DialogueList currentConversation;

    private int statementIndex = 0;
    private bool isPlaying = false;

    // separate dialogue input from interaction input by one frame...
    private bool goingToPlay = false;

	// Use this for initialization
	void Awake () {
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

        dialogueActionsText = new List<Button>();
	}

    public void SetDialogSequence(Dialogue.DialogueList sequence)
    {
        statementIndex = 0;
        currentConversation = sequence;
    }

    public void ActivateDialog()
    {
        dialogueCanvas.enabled = true;
        goingToPlay = true;
        statementIndex = 0;
        dialogueText.text = currentConversation.dialogStatements[0].statement;

        foreach (Button b in dialogueActionsText)
        {
            Destroy(b.gameObject);
        }

        dialogueActionsText.Clear();

        foreach(Dialogue.DialogueAction dAction in currentConversation.dialogueActions)
        {
            GameObject actionButton = Instantiate(buttonPrefab);
            Button button = actionButton.GetComponent<Button>();
            dialogueActionsText.Add(button);
            button.onClick.AddListener(dAction.action.Invoke);
            button.GetComponentInChildren<Text>().text = dAction.text;
        }

        // disable player controls while dialogue is happening
        pController.enabled = false;

        // Check if we're alraedy at the end (dialogue count is only one
        if (statementIndex == currentConversation.dialogStatements.Count - 1)
        {
            dialogueActionsText.ForEach((Button button) => 
                button.gameObject.transform.SetParent(GameObject.Find("ActionPanel").transform));
        }
    }

    public void HideDialogue()
    {
        isPlaying = false;
        goingToPlay = false;
        dialogueCanvas.enabled = false;
        pController.enabled = true;
    }

	// Update is called once per frame
	void Update () {

		if(isPlaying && Input.GetButtonDown("Interact"))
        {
            if(statementIndex < currentConversation.dialogStatements.Count - 1)
            {
                statementIndex++;
                dialogueText.text = currentConversation.dialogStatements[statementIndex].statement;
            }

            // We are on the last dialogue frame
            if (statementIndex == currentConversation.dialogStatements.Count - 1)
            {
                dialogueActionsText.ForEach((Button button) => button.gameObject.transform.SetParent(GameObject.Find("ActionPanel").transform));
            }

        }
        else if (goingToPlay && !isPlaying)
        {
            isPlaying = true;
        }
	}

}
