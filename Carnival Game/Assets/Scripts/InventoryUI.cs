using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {
    
    // I don't this really has to be a singleton, but may as well make it one to
    // be consistent with DialogueManager. We could easly remove the singleton-ness if we want.
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
                // Make the image visible
                invImage.color = new Color(255, 255, 255, 255);

                // Enables inspecting items in inventory
                // Button slotButton = slot.GetComponent<Button>();
                // slotButton.onClick.AddListener(obj.GetComponent<Dialogue>().TriggerDialog);
                break;
            }
        }
    }

    public void RemoveItem(GameObject obj)
    {
        SpriteRenderer sRenderer = obj.GetComponent<SpriteRenderer>();
        if (sRenderer == null)
        {
            // We won't have a sprite to remove.
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

                // Make the image color invisible so that it doesn't look ugly 
                invImage.color = new Color(255, 255, 255, 0);
                // slot.GetComponent<Button>().onClick.RemoveAllListeners();
                break;
            }
        }

        //CompressItems();
    }

    // We want to compress the items together whenever an element is 
    // removed from the UI
    private void CompressItems()
    {
        for(int i = 0; i < inventorySlots.Count; i++)
        {
            // Keep looping till we find an empty slot
            if(inventorySlots[i].GetComponent<Image>().sprite == null)
            {
                // Find a sprite to fill it with. If none are found,
                // We've compressed everything
                bool foundSprite = false;
                for(int j = i + 1; j < inventorySlots.Count; j++)
                {
                    Image foundImg = inventorySlots[j].GetComponent<Image>();

                    // If we find a sprite, swap them
                    if (foundImg.sprite != null)
                    {
                        foundSprite = true;
                        Image oldImg = inventorySlots[i].GetComponent<Image>();
                        oldImg.sprite = Sprite.Create(foundImg.sprite.texture, foundImg.sprite.rect, foundImg.sprite.pivot);
                        oldImg.color = new Color(255, 255, 255, 255);

                        foundImg.sprite = null;
                        foundImg.color = new Color(255, 255, 255, 0);
                        break;
                    }
                }
                if(!foundSprite)
                {
                    break;
                }
            }
        }
    }

}
