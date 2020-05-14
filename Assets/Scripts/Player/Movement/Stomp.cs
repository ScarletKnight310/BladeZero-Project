using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomp : MonoBehaviour
{
    private Rigidbody2D rb;
    private float usableTime;
    private bool stomp = false;
    private bool slamming = false;
    private float defGravity = 1f;

    [Header("Stomp Control")]
    public float force = 100;
    public float stompCooldown = 1f;
    [Space]
    [Header("Stomp Effect")]
    public GameObject stompEffect;

    void Awake() {
        if (stompEffect) // spawns offscreen, fix position later
            stompEffect = Instantiate(stompEffect, new Vector2(100, 100), Quaternion.identity);
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        defGravity = rb.gravityScale;
        usableTime = Time.time;
    }

    void Update()
    {
        if (Input.GetButton("Stomp") && (usableTime <= Time.time) && !PlayerPhysInfo.instance.onGround && !PlayerPhysInfo.instance.onWall) {
            GetComponent<JumpFix>().enabled = false;
            stomp = true;
            usableTime = Time.time + stompCooldown;
        }
            

        if(slamming && PlayerPhysInfo.instance.onGround) {
            if(stompEffect) {
                stompEffect.transform.position = new Vector2(transform.position.x, transform.position.y) + new Vector2(0, -PlayerPhysInfo.instance.offsetY);
                stompEffect.GetComponent<ParticleSystem>().Play();
            }
            GetComponent<JumpFix>().enabled = true;
            slamming = false;
        }

    }

    void FixedUpdate() {
        if(stomp) {
            rb.gravityScale = force;
            stomp = false;
            slamming = true;
        }
        if(PlayerPhysInfo.instance.onGround) {
            rb.gravityScale = defGravity;

        }
    }
}
