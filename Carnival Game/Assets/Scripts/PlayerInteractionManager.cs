using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionManager : MonoBehaviour {
    
    // Object we are focused on
    private GameObject focusedObject = null;

    // Reference to the player
    public GameObject player;

    // Inventory component of player
    private Inventory playerInventory;

    private void Start()
    {
        playerInventory = player.GetComponent<Inventory>();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Interact") && focusedObject)
        {
            InteractionObject interObj = focusedObject.GetComponent<InteractionObject>();
            bool meetsRequirements = playerInventory.CheckIfContainsRequiredItems(interObj.requiredObjects);
            Debug.Log("player meets requirements for " + interObj.name + ": " + meetsRequirements);
            if (interObj.OnInteract(meetsRequirements))
            {
                if(interObj.isPickup)
                {
                    addToPlayerInventory(focusedObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("InterObject"))
        {
            // Debug.Log("Focused object: " + other.name);
            focusedObject = other.gameObject;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        // Remove focused object if out of range
        // Only make the focused object null if it is the object we initially focused.
        // If it is a different object, then there are overlapping interactables.
        if (focusedObject == other.gameObject)
        {
            focusedObject = null;
        }
    }

    private void addToPlayerInventory(GameObject item)
    {
        playerInventory.AddItem(item);
    }


}
