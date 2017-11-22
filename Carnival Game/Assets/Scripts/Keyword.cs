using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Keyword : MonoBehaviour {

    public string expectedWord;

    public UnityEvent succeed;
    public UnityEvent fail;
    public RectTransform inputPanel;

    private InputField inputField;
    private Text placeHolderText;
    private Button doneButton;

	// Use this for initialization
	void Start () {
        inputPanel.gameObject.SetActive(false);
        inputField = inputPanel.transform.Find("InputPanel/InputField").gameObject.GetComponent<InputField>();
        placeHolderText = inputField.transform.Find("Placeholder").gameObject.GetComponent<Text>();
        doneButton = inputPanel.transform.Find("DoneButton").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void ShowInput()
    {
        // Enable the panel, and clean out any previous text
        inputPanel.gameObject.SetActive(true);
        inputField.text = "";
        inputField.characterLimit = expectedWord.Length;
        placeHolderText.text = "Enter a " + expectedWord.Length + " letter word";

        // Remove all old listeners, and add the listener to Validate
        // the input 
        doneButton.onClick.RemoveAllListeners();
        doneButton.onClick.AddListener(Validate);
    }

    public void Validate()
    {
        
        if(inputField.text.ToLower() == expectedWord.ToLower())
        {
            succeed.Invoke();
        }
        else
        {
            fail.Invoke();
        }

        inputPanel.gameObject.SetActive(false);
    }

}
