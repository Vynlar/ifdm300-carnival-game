using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Toggles the GameObject's collider and renderer components.
public class EnableDisableScript : MonoBehaviour {

	public bool disabled;
	private Renderer rend;
	private BoxCollider2D co2d;
	private ColliderReference colidRef;
	private EnableDisableScript glowScript;

	// Use this for initialization
	void Start () 
	{
		rend = GetComponent<Renderer>();
		co2d = GetComponent<BoxCollider2D>();
		colidRef = GetComponent<ColliderReference> ();
		GameObject glowObj = GameObject.Find(this.name+"Glow");
		if(glowObj!=null)glowScript = (EnableDisableScript) glowObj.GetComponent("EnableDisableScript");
			
		
		if (disabled)
			Disable ();
		else
			Enable ();
	}

	public void Enable()
	{
		if(rend!=null) rend.enabled = (true);
		if(co2d!=null) co2d.enabled = (true);
		if(colidRef != null) colidRef.EnableCollider ();
		if(glowScript != null) glowScript.Enable();
	}
		
	public void Disable()
	{
		if(rend!=null) rend.enabled = (false);
		if(co2d!=null) co2d.enabled = (false);
		if(colidRef != null) colidRef.DisableCollider ();
		if(glowScript != null) glowScript.Disable();
	}
}
