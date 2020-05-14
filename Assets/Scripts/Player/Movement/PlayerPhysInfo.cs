using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPhysInfo : MonoBehaviour
{
    [Header("Animation")]
    public Animator animator;
    [HideInInspector]
    public static PlayerPhysInfo instance = null;

    // RB of player
    [HideInInspector]
    private Rigidbody2D rb;
    private BasicMovement movement;

    [Header("RayCast Length")]
    // Movement Status 
    public float groundRay = 0.1f;
    public float wallRay = 0.1f;

    private float deadZone = 0.0000001f;
    [HideInInspector]
    public float offsetY; // so ray wont collide with player 
    [HideInInspector]
    public float offsetX; // so ray wont collide with player
    [HideInInspector]
    public bool onGround;
    [HideInInspector]
    public bool onWall_R;
    [HideInInspector]
    public bool onWall_L;
    [HideInInspector]
    public bool onWall;
    [HideInInspector]
    public bool onAny_sur;
    [HideInInspector]
    public int moveDirectionX;
    [HideInInspector]
    public int moveDirectionY;
    [HideInInspector]
    public bool lookingRight = true;
    [HideInInspector]
    public bool attacking_Melee = false;

    void Awake() {
        if (instance == null)
            instance = this;

        offsetY = GetComponent<Collider2D>().bounds.extents.y + 0.01f;
        offsetX = GetComponent<Collider2D>().bounds.extents.x + 0.01f;
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<BasicMovement>();
    }

    void Update() {
        //Debug.Log("G: "+onGround+" L: "+onWall_L+" R: "+ onWall_R);
        animator.SetBool("attacking_melee", attacking_Melee);
        animator.SetBool("onGround", onGround);
        animator.SetBool("onWall", onWall);

        moveDirectionX = Mathf.Abs(movement.x) < deadZone ? 0 : (int)Mathf.Sign(movement.x);
        animator.SetInteger("movement", moveDirectionX);

        moveDirectionY = Mathf.Abs(movement.y) < deadZone ? 0 : (int)Mathf.Sign(movement.y);

        if (moveDirectionX == 1 && !lookingRight || moveDirectionX == -1 && lookingRight)
           Flip();
    }

    void FixedUpdate() {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);

        onGround = Physics2D.Raycast(pos + new Vector2(0, -offsetY), Vector2.down, groundRay)
            || Physics2D.Raycast(pos + new Vector2(offsetX, -offsetY), Vector2.down, groundRay)
            || Physics2D.Raycast(pos + new Vector2(-offsetX, -offsetY), Vector2.down, groundRay)
            ? true : false;
        onWall_L = Physics2D.Raycast(pos + new Vector2(-offsetX, 0), Vector2.left, wallRay)
            || Physics2D.Raycast(pos + new Vector2(-offsetX, offsetY), Vector2.left, wallRay)
            || Physics2D.Raycast(pos + new Vector2(-offsetX, -offsetY), Vector2.left, wallRay)
            ? true : false;

        onWall_R = Physics2D.Raycast(pos + new Vector2(offsetX, 0), Vector2.right, wallRay)
            || Physics2D.Raycast(pos + new Vector2(offsetX, offsetY), Vector2.right, wallRay)
            || Physics2D.Raycast(pos + new Vector2(offsetX, -offsetY), Vector2.right, wallRay)
            ? true : false;

        onWall = onWall_L || onWall_R;
        onAny_sur = onWall || onGround;
    }

    void Flip() {
        lookingRight = !lookingRight;
        Vector3 charScale = transform.localScale;
        charScale.x *= -1;
        transform.localScale = charScale;
        if (onGround) {
            movement.moveEffect.transform.position = new Vector2(transform.position.x, transform.position.y) + new Vector2(0, -offsetY);
            movement.moveEffect.GetComponent<ParticleSystem>().Play();
        }
    }
}

