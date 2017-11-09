using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

public class AddDialogDefaults : ScriptableObject {

	[MenuItem ("Extra Scripts/Add HideDialogue")]
    static void AddDialogueDone()
    {
        DialogueManager dManager = null;
        GameObject[] bobjs = (GameObject[])Editor.FindObjectsOfType(typeof(GameObject));

        foreach (GameObject go in bobjs)
        {
            if(go.name == "DialogueManager")
            {
                dManager = go.GetComponent<DialogueManager>();
            }
        }

        if(!dManager)
        {
            Debug.Log("hey, couldn't find the dialogueManager");
            return;
        }
        Dialogue[] objs = (Dialogue[])Editor.FindObjectsOfType(typeof(Dialogue));
        foreach (Dialogue dLog in objs)
        {
            foreach (Dialogue.DialogueList dList in dLog.dQueues)
            {
                bool foundDone = false;
                foreach (Dialogue.DialogueAction dAction in dList.dialogueActions)
                {
                    if(dAction.text == "Done")
                    {
                        foundDone = true;
                        break;
                    }
                }
                if(!foundDone)
                {
                    Dialogue.DialogueAction action = new Dialogue.DialogueAction("Done", new UnityAction(dManager.HideDialogue));
                    //action.action

                    UnityEditor.Events.UnityEventTools.AddVoidPersistentListener(action.action, dManager.HideDialogue);
                    dList.dialogueActions.Add(action);
                }
            }
        }
    }

    //[MenuItem("Extra Scripts/RemoveHideDialogue")]
    static void RemoveDialogueDone()
    {
        DialogueManager dManager = null;
        GameObject[] bobjs = (GameObject[])Editor.FindObjectsOfType(typeof(GameObject));

        foreach (GameObject go in bobjs)
        {
            if (go.name == "DialogueManager")
            {
                dManager = go.GetComponent<DialogueManager>();
            }
        }

        if (!dManager)
        {
            Debug.Log("hey, couldn't find the dialogueManager");
            return;
        }
        Dialogue[] objs = (Dialogue[])Editor.FindObjectsOfType(typeof(Dialogue));
        foreach (Dialogue dLog in objs)
        {
            foreach (Dialogue.DialogueList dList in dLog.dQueues)
            {
                foreach (Dialogue.DialogueAction dAction in dList.dialogueActions)
                {
                    if (dAction.text == "Done")
                    {
                        dList.dialogueActions.Remove(dAction);
                    }
                }
            }
        }
    }

}
