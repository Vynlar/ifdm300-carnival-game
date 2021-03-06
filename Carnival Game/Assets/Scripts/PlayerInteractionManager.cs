﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionManager : MonoBehaviour {
    
    // Object we are focused on
    private GameObject focusedObject = null;

    // Reference to the player
    public GameObject player;

    // Collider to use for interaction triggers
    public Collider2D interactionCollider;

    // Color to use when highlighting objects
    public Color itemHighlightColor;

    // Material for the sprite to go back to after it's
    // no longer being highlighted.
    public Material defaultSpriteMaterial;

    // Inventory component of player
    private Inventory playerInventory;

    // List of objects that the player is currently intersecting with
    private List<GameObject> objectsInRange;

    private void Start()
    {
        objectsInRange = new List<GameObject>();
        playerInventory = player.GetComponent<Inventory>();
    }

    private void Update()
    {
        UpdateFocusedObject();
    }

    private void UpdateFocusedObject()
    {
        GameObject prevFocusedObject = focusedObject;
        focusedObject = null;

        // Compare distances and focus on the closest interactable
        float minDistance = float.MaxValue;

        foreach (GameObject go in objectsInRange)
        {
            float dist = Vector3.Distance(go.transform.position, transform.position);
            if (dist < minDistance && go.GetComponent<InteractionObject>().GetInteractable())
            {
                minDistance = dist;
                focusedObject = go;
            }
        }

        if(focusedObject != prevFocusedObject)
        {
            if(focusedObject != null)
            {
                SpriteGlow.SpriteGlow sGlow = focusedObject.AddComponent<SpriteGlow.SpriteGlow>();
                sGlow.GlowColor = itemHighlightColor;
                sGlow.OutlineWidth = 8;
            }
            if(prevFocusedObject != null)
            {
                prevFocusedObject.GetComponent<SpriteRenderer>().material = defaultSpriteMaterial;
                Destroy(prevFocusedObject.GetComponent<SpriteGlow.SpriteGlow>());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Make sure our InteractionCollider is the one that is overlapping
        if(other.IsTouching(interactionCollider) && other.GetComponent<InteractionObject>())
        {
            if(!objectsInRange.Contains(other.gameObject))
            {
                objectsInRange.Add(other.gameObject);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        // Make sure our interactionCollider is the collider that left
        if (!other.IsTouching(interactionCollider))
        {
            objectsInRange.Remove(other.gameObject);
        }
    }

    private void addToPlayerInventory(GameObject item)
    {
        playerInventory.AddItem(item);
    }

    public void Interact()
    {
        if (focusedObject)
        {
            InteractionObject interObj = focusedObject.GetComponent<InteractionObject>();
            bool meetsRequirements = playerInventory.CheckIfContainsRequiredItems(interObj.requiredObjects);
            if (interObj.OnInteract(meetsRequirements))
            {
                if (interObj.isPickup)
                {
                    addToPlayerInventory(focusedObject);
                }
            }
        }
    }
}
