using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    // List of inventory items
    private List<GameObject> inventory;

	// Use this for initialization
	void Start () {
        inventory = new List<GameObject>();
	}

    public void AddItem(GameObject item)
    {
        item.GetComponent<SpriteRenderer>().enabled = false;
        item.GetComponent<InteractionObject>().SetInteractable(false);
        inventory.Add(item);

        foreach(GameObject go in inventory)
        {
            Debug.Log(go.name);
        }
    }

    public void RemoveItem(GameObject item)
    {
        inventory.Remove(item);
    }

    public bool CheckIfContainsRequiredItems(GameObject[] requiredObjs)
    {
        //Debug.Log("requiredObj size: " + requiredObjs.Length);
        // No objects are required
        if (requiredObjs.Length == 0)
        {
            //Debug.Log("No items required");
            return true;
        }

        for (int i = 0; i < requiredObjs.Length; i++)
        {
            bool found = false;
            for (int j = 0; j < inventory.Count; j++)
            {
                Debug.Log("Comparing: " + requiredObjs[i].name + " vs " + inventory[j].name);

                if(requiredObjs[i].name == inventory[j].name)
                {
                    found = true;
                    Debug.Log("Found " + requiredObjs[i].name + "in inventory: " + inventory[j].name);
                }
            }
            if (!found)
            {
                return false;
            }
        }
        return true;
    }

}
