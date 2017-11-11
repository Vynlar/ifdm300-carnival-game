using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {
    
    // I don't this really has to be a singleton, but may as well make it one to
    // be consistent with DialogueManager
    public static InventoryUI Instance;

    // List of all inventory slots
    public List<GameObject> inventorySlots;

	// Use this for initialization
	void Start () {
		if(Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
	}

    // Since we're just dealing with 2D,
    // we can just add the sprite for now.
    public void AddItem(GameObject obj)
    {
        SpriteRenderer sRenderer = obj.GetComponent<SpriteRenderer>();
        if(sRenderer == null)
        {
            // We won't have a sprite to add.
            Debug.Log("Tried to add item to inventory without a sprite");
            return;
        }

        // find the first button without an image, and 
        // add the sprite to it.
        foreach(GameObject slot in inventorySlots)
        {
            Image invImage = slot.GetComponent<Image>();
            if (invImage.sprite == null)
            {
                invImage.sprite = sRenderer.sprite;
                invImage.color = new Color(255, 255, 255, 255);
                break;
            }
        }
    }

    public void RemoveItem(GameObject obj)
    {
        SpriteRenderer sRenderer = obj.GetComponent<SpriteRenderer>();
        if (sRenderer == null)
        {
            // We won't have a sprite to add.
            Debug.Log("Tried to remove an item without a sprite");
            return;
        }

        // Set inventory slot back to normal if we find the object sprite.
        foreach (GameObject slot in inventorySlots)
        {
            Image invImage = slot.GetComponent<Image>();
            if (invImage != null && invImage.sprite.Equals(sRenderer.sprite))
            {
                invImage.sprite = null;
                invImage.color = new Color(255, 255, 255, 0);
                break;
            }
        }
    }

}
