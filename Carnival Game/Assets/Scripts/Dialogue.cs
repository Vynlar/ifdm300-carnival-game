using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

// Component used for NPC's to allow for dialog.
// Uses a dictionary of states->dialog queues to allow dialog 
// to change based on the state of the game (or character)


public class Dialogue : MonoBehaviour {

    // Might be good to figure out a better way to make states. At least with this, 
    // we can make as many different states per NPC as we want... strings might be 
    // hard to keep track of
    public string currentState;

    // Diaglog statements, with optional animations attached to each.
    [Serializable]
    public class DialogueStatement
    {
        [TextArea(3,10)] // Makes text fields larger for dialog
        public string statement;
    }

    // All possible dialog streams
    [Serializable]
    public class DialogueList
    {
        public string StateKey;
        public List<DialogueStatement> dialogStatements;
        public List<DialogueAction> dialogueActions;
    }

    [Serializable]
    public class DialogueAction
    {
        public string text;
        public UnityEvent action;

        public DialogueAction(string text, UnityAction dAction)
        {
            this.text = text;
            this.action = new UnityEvent();
            action.AddListener(dAction);
        }
    }

    // This is used for the player to put dialog stuff into.
    // (Because it is serializable)
    public DialogueList[] dQueues;

    // We won't populate this in the inspector becuase it isn't serializeable.
    private Dictionary<string, DialogueList> dialogTrees;
    
	// Use this for initialization
	void Start ()
    {

        // Instantiate new Dictionary
        dialogTrees = new Dictionary<string, DialogueList>();

        // populate it with the list of stuff the user made.
        foreach(DialogueList dlist in dQueues)
        {
           //dlist.dialogueActions.Add(new DialogueAction("Done", 
                //new UnityAction(DialogueManager.Instance.HideDialogue)));

            dialogTrees.Add(dlist.StateKey, dlist);
        }


    }

    // Sets the current string array to use for dialog
    public void SetDialogState(string state)
    {
        currentState = state;
    }

    // Activate the dialogue manager
    public void TriggerDialog()
    {
        DialogueManager.Instance.SetDialogSequence(dialogTrees[currentState]);
        DialogueManager.Instance.ActivateDialog();
    }
}
