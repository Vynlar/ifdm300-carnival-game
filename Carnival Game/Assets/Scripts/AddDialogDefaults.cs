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
                Dialogue.DialogueAction newAction = null;
                foreach (Dialogue.DialogueAction dAction in dList.dialogueActions)
                {
                    if(dAction.text == "Done")
                    {
                        if(dAction != dList.dialogueActions[dList.dialogueActions.Count - 1])
                        {
                            if(newAction == null)
                            {
                                newAction = dAction;
                            }
                            dList.dialogueActions.Remove(dAction);
                        }
                        else if (dAction == dList.dialogueActions[dList.dialogueActions.Count - 1])
                        {
                            foundDone = true;
                        }
                    }
                }
                if(!foundDone)
                {
                    if (newAction != null)
                    {
                        dList.dialogueActions.Add(newAction);
                    }
                    else
                    {
                        Dialogue.DialogueAction action = new Dialogue.DialogueAction("Done", new UnityAction(dManager.HideDialogue));
                        UnityEditor.Events.UnityEventTools.AddVoidPersistentListener(action.action, dManager.HideDialogue);
                        dList.dialogueActions.Add(action);
                    }
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
