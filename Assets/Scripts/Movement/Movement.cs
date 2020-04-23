using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float x = 0;
    private bool jumping = false;

    [Header("External Hit Boxes")]
    public GameObject jumpCheck;

    [Space]
    [Header("Player Movement Modifier")]
    public float speed, jumpHei = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal");
        if (Input.GetButton("Jump") && jumpCheck.GetComponent<MoveCheck>().col)
            jumping = true;
    }

    void FixedUpdate() { 
        Walk(x);
        if (jumping) {
            Jump();
            jumping = false;
        }
    }

    void Walk(float moveMod) { 
        rb.velocity = new Vector2(moveMod * speed, rb.velocity.y);
    }

    void Jump() 
    {
        rb.velocity = new Vector2(rb.velocity.x,jumpHei);
    }
}
