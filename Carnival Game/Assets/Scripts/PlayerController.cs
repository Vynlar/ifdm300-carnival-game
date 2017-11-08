﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public float maxRunSpeed = 100;
    public float runSpeed = 10;

    private Rigidbody2D rb2d; // Character's rigidbody
    private PlayerInteractionManager playerIM; // InteractionManager

    void Awake()
    {
        // Get component references
        rb2d = GetComponent<Rigidbody2D>();
        playerIM = GetComponent<PlayerInteractionManager>();
    }

    private void Update()
    {
        // Test for interaction input
        if (Input.GetButtonDown("Interact"))
        {
            playerIM.Interact();
        }
    }

    // Using FixedUpdate for physics
    void FixedUpdate () {

        // Check for left and right input 
        if (Input.GetKey(KeyCode.D))
        {
            rb2d.AddForce(new Vector2(runSpeed, 0));
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb2d.AddForce(new Vector2(-runSpeed, 0));
        }

        // Cap the run speed
        if(Mathf.Abs(rb2d.velocity.x) > maxRunSpeed)
        {
            rb2d.velocity = rb2d.velocity.x < 0 ? new Vector2(-maxRunSpeed, rb2d.velocity.y) : 
                new Vector2(maxRunSpeed, rb2d.velocity.y);
        }
    }
}
