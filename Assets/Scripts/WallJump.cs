using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject leftCheck;
    public GameObject rightCheck;
    public float wallSlideSpeed = 5f;
    public float wallJumpMult = 2f;
    public float wallJumpXp = 1f;
    public float wallJumpYp = 0.75f;
    public bool wallSlide = false;
    private float dirX = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        RaycastHit2D nearWall;
        bool onWall = leftCheck.GetComponent<MoveCheck>().col || rightCheck.GetComponent<MoveCheck>().col;
        float yVelo = rb.velocity.y;

        if (onWall) {
            dirX = leftCheck.GetComponent<MoveCheck>().col ? -1 : 1;
            GetComponent<BetterJump>().enabled = false;
            if (rb.velocity.y < 0 && !((GetComponent<Movement>().jumpCheck).GetComponent<MoveCheck>().col)) {
                wallSlide = true;
                if (rb.velocity.y < -wallSlideSpeed) {
                    rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
                }
            }
            if (wallSlide && Input.GetButton("Jump")) {
                //rb.AddForce(new Vector2(dirX * wallJump.x, wallJump.y));
                float jpPower = GetComponent<Movement>().jumpHei;
                rb.velocity = new Vector2(-dirX * jpPower * wallJumpMult * wallJumpXp, jpPower * wallJumpMult * wallJumpYp);
            }
        } else {
            GetComponent<BetterJump>().enabled = true;
            wallSlide = false;
            rb.velocity = new Vector2(rb.velocity.x, yVelo);
        }
    }


}
