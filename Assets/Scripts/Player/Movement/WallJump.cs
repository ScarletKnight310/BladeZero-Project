using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool wallJumping = false;

    [Header("Wall Movement Values")]
    public float wallSlideSpeed = 5f;
    public float wallJumpLerp = .5f;
    public Vector2 WallLeap = new Vector2(1, 1);// The wall jump
    
    [Space]
    [Header("WallJump Effect")]
    public GameObject wallJumpEffect;

    void Awake() {
        if (wallJumpEffect) // spawns offscreen, fix position later
            wallJumpEffect = Instantiate(wallJumpEffect, new Vector2(100, 100), Quaternion.identity);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {

        if (wallJumpEffect) {
            wallJumpEffect.transform.position = rb.transform.position;
        }

        if (Input.GetButton("Jump") && !PlayerPhysInfo.instance.onGround && PlayerPhysInfo.instance.onWall) {
            wallJumping = true;
            if (wallJumpEffect)
                wallJumpEffect.GetComponent<ParticleSystem>().Play();
        }

        if (wallJumpEffect && PlayerPhysInfo.instance.onGround) {
            wallJumpEffect.GetComponent<ParticleSystem>().Stop();
        }

        /*
        if(lastWall == PlayerPhysInfo.instance.moveDirection) {
            sameWall = true;
        }       
        else if (PlayerPhysInfo.instance.onGround) {
            sameWall = false;
            lastWall = 3;// to insure we can't wall climb
        }

        Debug.Log("SW: " +sameWall);
        */
    }
    void FixedUpdate()
    {

        if (PlayerPhysInfo.instance.onWall && !PlayerPhysInfo.instance.onGround) {
            float yVelo = rb.velocity.y;
            if (yVelo < 0) {
                // Wall Slide
                if (yVelo < -wallSlideSpeed) {
                    rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
                }
                else {
                    rb.velocity = new Vector2(rb.velocity.x, yVelo);
                }
            }
            
            if (wallJumping) { //  !sameWall
                WallMove();
            }
            
        }
    }

    public void WallMove() {
        int dir = PlayerPhysInfo.instance.moveDirectionX;
        
        if (PlayerPhysInfo.instance.onWall_L && dir == 1) {
           // rb.velocity = new Vector2(dir * WallLeap.x, WallLeap.y);
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir * WallLeap.x, WallLeap.y)), wallJumpLerp);
        } 
        else if (PlayerPhysInfo.instance.onWall_R && dir == -1){
            // rb.velocity = new Vector2(dir * WallLeap.x, WallLeap.y);
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir * WallLeap.x, WallLeap.y)), wallJumpLerp);
        }

        wallJumping = false;
    }

}