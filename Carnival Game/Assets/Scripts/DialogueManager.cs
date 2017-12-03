using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    // Static instance of this manager (singleton)
    public static DialogueManager Instance;

    // Canvas that holds the dialogue elements
    public GameObject dialoguePanel;

    // Text that will be inside the dialog box.
    public Text dialogueText;

    // Characters displayed per second
    public float playSpeed = 10;

    // Prefab to use for action buttons
    public GameObject buttonPrefab;

    // Reference to player 
    public PlayerController pController;

    // The buttons for each dialogue action
    private List<Button> dialogueActionsText;

    // Text conversation to play
    private Dialogue.DialogueList currentConversation;

    // Current string index we are using
    private int statementIndex = 0;

    // Whether the dialogue window is being displayed
    private bool isPlaying = false;

    // separate dialogue input from interaction input by one frame...
    private bool goingToPlay = false;

    // is the dialogue text still rolling? 
    private bool isRolling = false;

    // Button that is focused (for keys)
    private int focusedButtonIndex = 0;

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
        dialoguePanel.SetActive(false);

        dialogueActionsText = new List<Button>();
	}

    // Update is called once per frame
    void Update()
    {
        if(isPlaying && pController.ControlsEnabled())
        {
            pController.SetControlsEnabled(false);
        }

        if (isPlaying && Input.GetButtonDown("Interact"))
        {
            
            // Finish up showing dialogue and return
            if(isRolling)
            {
                StopAllCoroutines();
                isRolling = false;
                dialogueText.text = currentConversation.dialogStatements[statementIndex].statement;

                // We are on the last dialogue frame
                if (statementIndex == currentConversation.dialogStatements.Count - 1)
                {
                    SetUpButtons();
                }
                return;
            }
            if (statementIndex < currentConversation.dialogStatements.Count - 1)
            {
                statementIndex++;
                StopAllCoroutines();
                StartCoroutine(RollDialog());
            }
        }
        else if (goingToPlay && !isPlaying)
        {
            isPlaying = true;
        }

        if(isPlaying && statementIndex == currentConversation.dialogStatements.Count - 1 && !isRolling)
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                dialogueActionsText[focusedButtonIndex].targetGraphic.color = new Color(1, 1, 1);

                focusedButtonIndex--;
                if(focusedButtonIndex < 0)
                {
                    focusedButtonIndex = dialogueActionsText.Count - 1;
                }

                dialogueActionsText[focusedButtonIndex].targetGraphic.color = new Color(0.8f, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                dialogueActionsText[focusedButtonIndex].targetGraphic.color = new Color(1, 1, 1);

                focusedButtonIndex++;
                if (focusedButtonIndex >= dialogueActionsText.Count)
                {
                    focusedButtonIndex = 0;
                }

                dialogueActionsText[focusedButtonIndex].targetGraphic.color = new Color(0.8f, 0, 0);
            }
        }
    }

    public void SetDialogSequence(Dialogue.DialogueList sequence)
    {
        statementIndex = 0;
        currentConversation = sequence;
    }

    public void ActivateDialog()
    {
        dialoguePanel.SetActive(true);
        goingToPlay = true;
        statementIndex = 0;
        StopAllCoroutines();
        StartCoroutine(RollDialog());

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
        pController.SetControlsEnabled(false);

        // Check if we're alraedy at the end (dialogue count is only one)
        if (statementIndex == currentConversation.dialogStatements.Count - 1 && !isRolling)
        {
            SetUpButtons();
        }
    }
    IEnumerator RollDialog()
    {
        dialogueText.text = "";
        isRolling = true;
        foreach(char letter in currentConversation.dialogStatements[statementIndex].statement.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(1 / playSpeed);
        }
         isRolling = false;

        // We are on the last dialogue frame
        if (statementIndex == currentConversation.dialogStatements.Count - 1)
        {
            SetUpButtons();
        }
    }

    public void HideDialogue()
    {
        isPlaying = false;
        goingToPlay = false;
        dialoguePanel.SetActive(false);
        pController.SetControlsEnabled(true);
    }

    private void SetUpButtons()
    {
        foreach (Button button in dialogueActionsText)
        {
            button.gameObject.transform.SetParent(GameObject.Find("ActionPanel").transform, false);
            button.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        focusedButtonIndex = 0;
        dialogueActionsText[focusedButtonIndex].targetGraphic.color = dialogueActionsText[focusedButtonIndex].targetGraphic.color = new Color(0.8f, 0, 0);

    }
}
