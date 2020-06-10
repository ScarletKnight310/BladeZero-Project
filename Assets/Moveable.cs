using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    private Rigidbody2D rb;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "Player")
            rb.velocity = -col.gameObject.GetComponent<Rigidbody2D>().velocity;
    }

    private void OnCollisionStay2D(Collision2D col) {
        if (col.gameObject.tag == "Player")
            rb.velocity = -col.gameObject.GetComponent<Rigidbody2D>().velocity;
    }
}
