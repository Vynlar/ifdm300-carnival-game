using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Keyword : MonoBehaviour {

    // The word that is needed to trigger success events
    public string expectedWord;

    // Events to trigger during succeed or fail
    public UnityEvent succeed;
    public UnityEvent fail;

    // References to important UI elements
    public RectTransform basePanel;
    public Transform charPanelGroup;
    public GameObject charPanelPrefab;

    // List of text objects in character panels
    private List<GameObject> charPanels;
    private List<Text> charPanelText;

    // Other important elements that we find on start
    private Button doneButton;

    private string enteredKeyword;
    private int currentCharIndex = 0;
    private bool isTyping = false;

	// Use this for initialization
	void Start () {

		initializeAsNew();
    }

	void initializeAsNew()
	{
		charPanels = new List<GameObject>();
		charPanelText = new List<Text>();

		// make the panel invisable on start
		basePanel.gameObject.SetActive(false);

		// Clean out any existing character panels
		ClearCharPanels();

		// Get the references to the UI elements that we need
		doneButton = basePanel.transform.Find("DoneButton").GetComponent<Button>();
	}
		
    // Update is called once per frame
    void Update () {

        if(!isTyping || !basePanel.gameObject.activeSelf)
        {
            // We aren't typing the keyword, or the panel isn't active, so return
            return;
        }

        // Disable player input if it is enabled
        if (DialogueManager.Instance.pController.ControlsEnabled())
        {
            DialogueManager.Instance.pController.SetControlsEnabled(false);
        }

        if (Input.anyKeyDown)
        {
            char inChar = '=';
            if(Input.inputString != "")
            {
                inChar = Input.inputString.ToCharArray()[0];
            }

            // If the input character was a letter
            if((inChar >= 'a' && inChar <= 'z') || (inChar >= 'A' && inChar <= 'Z'))
            {
                if(currentCharIndex == charPanels.Count - 1 &&
                    charPanelText[currentCharIndex].text != "" )
                {
                    return;
                }
                // We've typed a valid input character
                charPanelText[currentCharIndex].text = inChar.ToString().ToUpper();

                // Increment the index if possible
                if(currentCharIndex < charPanels.Count - 1)
                {
                    currentCharIndex++;
                }
            }

            // If the user hits backspace... 
            else if (Input.GetKeyDown(KeyCode.Backspace))
            {
                if (currentCharIndex > 0)
                {
                    if(currentCharIndex == charPanels.Count - 1 &&
                        charPanelText[currentCharIndex].text != "")
                    {
                        charPanelText[currentCharIndex].text = "";
                        return;
                    }
                    currentCharIndex--;
                }

                // Clear char panel text
                charPanelText[currentCharIndex].text = "";
            }

            // If the user hits enter...
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                doneButton.onClick.Invoke();
            }
        }
	}

    public void ShowInput()
    {
        // Disable the player controller
        DialogueManager.Instance.pController.SetControlsEnabled(false);

        // Clean out any previous charPanels and put in new ones.
        RepopulateCharPanels();

        // Enable the panel
        basePanel.gameObject.SetActive(true);
        isTyping = true;

        // Remove all old listeners, and add the listener to Validate
        // the input 
        doneButton.onClick.RemoveAllListeners();
        doneButton.onClick.AddListener(Validate);
    }

    public void Validate()
    {
        // Put together our final validation string
        foreach(Text text in charPanelText)
        {
            enteredKeyword += text.text;
        }

        if(enteredKeyword.ToUpper() == expectedWord.ToUpper())
        {
            succeed.Invoke();
			initializeAsNew();
        }
        else
        {
            fail.Invoke();
        }

        basePanel.gameObject.SetActive(false);
        isTyping = false;
        enteredKeyword = "";

        // Re-enable controls only if there's no dialog playing
        if(!DialogueManager.Instance.dialoguePanel.activeSelf)
        {
            DialogueManager.Instance.pController.SetControlsEnabled(true);
        }
    }

    private void ClearCharPanels()
    {
        foreach (GameObject go in charPanels)
        {
            Destroy(go);
        }
        charPanels.Clear();

        // We already destroyed the parent, no need to destroy again.
        charPanelText.Clear();

        currentCharIndex = 0;
    }

    private void RepopulateCharPanels()
    {
        ClearCharPanels();

        // Add in new char panels
        for (int i = 0; i < expectedWord.Length; i++)
        {
            GameObject charP = Instantiate(charPanelPrefab, charPanelGroup, false);
            charPanels.Add(charP);
            charPanelText.Add(charP.transform.Find("Elements/Character").GetComponent<Text>());
        }

    }

    public void SetExpectedWord(string word)
    {
        expectedWord = word;
        RepopulateCharPanels();
    }
}
