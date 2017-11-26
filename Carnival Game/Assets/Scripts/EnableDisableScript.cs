using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Toggles the GameObject's collider and renderer components.
public class EnableDisableScript : MonoBehaviour {

	public bool disabled;
	private Renderer rend;
	private BoxCollider2D co2d;
	private ColliderReference colidRef;

	// Use this for initialization
	void Start () 
	{
		rend = GetComponent<Renderer>();
		co2d = GetComponent<BoxCollider2D>();
		colidRef = GetComponent<ColliderReference> ();
		if (disabled)
			Disable ();
		else
			Enable ();
	}

	public void Enable()
	{
		rend.enabled = (true);
		co2d.enabled = (true);
		if(colidRef != null) 
			colidRef.EnableCollider ();
	}
		
	public void Disable()
	{
		rend.enabled = (false);
		co2d.enabled = (false);
		if(colidRef != null) 
			colidRef.DisableCollider ();
	}
}
