using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [HideInInspector]
    public float x = 0;
    [HideInInspector]
    public float y = 0;
    [HideInInspector]
    public bool jump = false;
    [HideInInspector]
    public bool doubleJump = false;
    [HideInInspector]
    public bool ableToJump = false;

    [Header("Movement Modifier")]
    public float moveSpeed = 5f;

    [Space]
    public bool doubleJumpEnabled = false;
    public float jumpForce = 5f;
    public float midAirMult = 1.2f;

    [Space]
    [Header("Movement Effect")]
    public GameObject moveEffect;

    void Awake() {
        if (moveEffect) // spawns offscreen, fix position later
            moveEffect = Instantiate(moveEffect, new Vector2(100, 100), Quaternion.identity);
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        if (PlayerPhysInfo.instance.onGround) {
            ableToJump = true;
            if (Input.GetButton("Jump"))
                jump = true;
        }
        else if (doubleJumpEnabled && Input.GetButton("Jump") && ableToJump) {
            doubleJump = true;
        }
    }

    void FixedUpdate() {
        if (PlayerPhysInfo.instance.onGround)
            Walk(x, 1);
        else
            Walk(x, midAirMult);
        if (jump) {
            Jump(Vector2.up);
            jump = false;
        }
        else if (doubleJump) {
            Jump(Vector2.up);
            doubleJump = false;
            ableToJump = false;
        }
    }

    void Walk(float dir, float modi) {
       if (PlayerPhysInfo.instance.onGround)
            rb.velocity = new Vector2(dir * moveSpeed, rb.velocity.y);
       else
            rb.velocity = new Vector2(dir * (moveSpeed * modi), rb.velocity.y);
    }

     void Jump(Vector2 dir) {
        if (PlayerPhysInfo.instance.onGround) {
            moveEffect.transform.position = new Vector2(transform.position.x, transform.position.y) + new Vector2(0, -PlayerPhysInfo.instance.offsetY);
            moveEffect.GetComponent<ParticleSystem>().Play();            
        }
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.velocity += dir * jumpForce;
     }
}
