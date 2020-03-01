using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
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
        float x = Input.GetAxis("Horizontal");
        Walk(x);
        if (Input.GetButton("Jump") && jumpCheck.GetComponent<MoveCheck>().col)
            Jump();
    }

    void Walk(float moveMod) 
    {
        rb.velocity = new Vector2(moveMod * speed, rb.velocity.y);
    }

    void Jump() 
    {
       rb.AddForce(new Vector2(0, 1) * jumpHei, ForceMode2D.Impulse);
    }
}
