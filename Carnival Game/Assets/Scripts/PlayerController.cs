using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public float maxRunSpeed = 100;
    public float runSpeed = 10;

    private Rigidbody2D rb2d; // Character's rigidbody
    private PlayerInteractionManager playerIM; // InteractionManager

    private Animator animator;

    bool controlsEnabled = true;

    void Awake()
    {
        // Get component references
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        playerIM = GetComponent<PlayerInteractionManager>();
    }

    private void Update()
    {
        if(controlsEnabled)
        {
            // Test for interaction input
            if (Input.GetButtonDown("Interact"))
            {
                playerIM.Interact();
            }
        }
    }

    // Using FixedUpdate for physics
    void FixedUpdate () {

        // Check for left and right input 
        if(controlsEnabled)
        {
            if (Input.GetKey(KeyCode.D))
            {
                rb2d.AddForce(new Vector2(runSpeed, 0));
                Vector2 newScale = this.transform.localScale;
                newScale.x = Mathf.Abs(newScale.x);
                this.transform.localScale = newScale;
            }
            if (Input.GetKey(KeyCode.A))
            {
                rb2d.AddForce(new Vector2(-runSpeed, 0));
                Vector2 newScale = this.transform.localScale;
                newScale.x = -Mathf.Abs(newScale.x);
                this.transform.localScale = newScale;
            }
        }

        // Cap the run speed
        if(Mathf.Abs(rb2d.velocity.x) > maxRunSpeed)
        {
            rb2d.velocity = rb2d.velocity.x < 0 ? new Vector2(-maxRunSpeed, rb2d.velocity.y) : 
                new Vector2(maxRunSpeed, rb2d.velocity.y);
        }


        if(Mathf.Abs(rb2d.velocity.x) > 0.2)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            animator.SetBool("walk", true);
            animator.speed = Mathf.Abs(rb2d.velocity.x/1.8f);
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
            animator.SetBool("walk", false);
        }
    }

    public void SetControlsEnabled(bool isEnabled)
    {
        controlsEnabled = isEnabled;
    }

    public bool ControlsEnabled()
    {
        return controlsEnabled;
    }
}
