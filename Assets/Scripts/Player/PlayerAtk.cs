using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerAtk : MonoBehaviour
{
    private Rigidbody2D rb;
    private float usableTime;
    public float atkCooldown = 1f;
    public LayerMask target;
    [Header("Melee Attack")]
    public Vector2 meleeRange = new Vector2(1,1);

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        usableTime = Time.time;
    }

    void Update()
    {
        if ((usableTime <= Time.time) && !PlayerPhysInfo.instance.attacking_Melee) {
            if (Input.GetButton("MeleeAtk")) { 
                PlayerPhysInfo.instance.attacking_Melee = true;
                usableTime = Time.time + atkCooldown;
            }
        }
    }

    void FixedUpdate() {
        if (PlayerPhysInfo.instance.attacking_Melee) {
            PlayerPhysInfo.instance.attacking_Melee = false;
            MeleeAtk();
        }
    }

    void MeleeAtk() {
        Collider2D[] ememiesHit = Physics2D.OverlapBoxAll(rb.position, meleeRange, target);
        for(int i = 0; i < ememiesHit.Length; i++) {
            Debug.Log("Hit"+ i);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(rb.position, meleeRange);
    }
}
