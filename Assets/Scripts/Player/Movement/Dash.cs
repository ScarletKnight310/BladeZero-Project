using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private Rigidbody2D rb;
    private float usableTime;
    private GameObject current;
    private bool dashing = false;

    [Header("Dash Control")]
    public float dashSpeed = 500f;
    public float dashCooldown = 1f;

    [Space]
    [Header("Dash Effect")]
    public GameObject dashEffect;

    void Awake() {
        if (dashEffect) // spawns offscreen, fix position later
            dashEffect = Instantiate(dashEffect, new Vector2(100,100), Quaternion.identity);
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        usableTime = Time.time;
    }

    void Update()
    {
        if ((usableTime <= Time.time) && Input.GetButton("Dash")) 
        {   
            dashing = true;
            if (dashEffect) {
                dashEffect.transform.position = rb.transform.position;
                dashEffect.GetComponent<ParticleSystem>().Play();
            }
            usableTime = Time.time + dashCooldown;
        }
    }

    void FixedUpdate() {
        if (dashing) {
            if (PlayerPhysInfo.instance.lookingRight) {
                rb.velocity = new Vector2(dashSpeed + Mathf.Abs(rb.velocity.x), 0);
                Debug.Log(dashSpeed +", "+ Mathf.Abs(rb.velocity.x));
            } else
                rb.velocity = new Vector2(-(dashSpeed + Mathf.Abs(rb.velocity.x)), 0);
            dashing = false;
        }
    }
}
