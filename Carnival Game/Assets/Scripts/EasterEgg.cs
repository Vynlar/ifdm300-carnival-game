using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEgg : MonoBehaviour {
    private SpriteRenderer currentSprite;
    private bool isAdam = false;
    public Sprite adamTurner;
    public Sprite puppetMaster;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        currentSprite = gameObject.GetComponent<SpriteRenderer>();
        if (Input.GetMouseButtonDown(0))
        {
            isAdam = !isAdam;
            if (isAdam)
            {
                currentSprite.sprite = puppetMaster;
            }
            if (!isAdam)
            {
                currentSprite.sprite = adamTurner;
            }
        }
    }
}
