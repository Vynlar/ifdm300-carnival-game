using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEgg : MonoBehaviour {
    private SpriteRenderer currentSprite;
    private bool isAdam = false;
    public Sprite adamTurner;
    public Sprite puppetMaster;
    public GameObject jen;

    // Use this for initialization
    void Start () {
        //Physics2D.IgnoreCollision(jen.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
    }
	
	// Update is called once per frame
	void Update () {
     
    }

    void OnMouseDown()
    {
        currentSprite = gameObject.GetComponent<SpriteRenderer>();
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
