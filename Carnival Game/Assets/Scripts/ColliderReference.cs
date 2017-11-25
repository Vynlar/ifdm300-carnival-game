using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderReference : MonoBehaviour {

    public BoxCollider2D colliderRef;

	public void DisableCollider()
    {
        colliderRef.enabled = false;
    }
	
	public void EnableCollider()
    {
        colliderRef.enabled = true;
    }
}
