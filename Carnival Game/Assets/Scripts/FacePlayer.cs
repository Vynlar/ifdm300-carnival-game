using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour {

    public enum ScaleDirection
    {
        Positive,
        Negative
    }

    public GameObject player;

    [Tooltip("Should this object be scaled positive when facing left?")]
    public ScaleDirection scaleWhenLeft = ScaleDirection.Positive;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // If player is to the left of this object
		if(player.transform.position.x < transform.position.x)
        {
            if (scaleWhenLeft == ScaleDirection.Positive)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), 
                    transform.localScale.y, transform.localScale.z);

            }
            else
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x),
                    transform.localScale.y, transform.localScale.z);
            }
        }

        // If the player is to the right of this object
        else if (player.transform.position.x >= transform.position.x)
        {
            if (scaleWhenLeft == ScaleDirection.Positive)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x),
                    transform.localScale.y, transform.localScale.z);

            }
            else
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x),
                    transform.localScale.y, transform.localScale.z);
            }
        }
    }
}
