using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCheck : MonoBehaviour
{
    public bool col = false;
    public string trigger = "Ground";

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == trigger)
            col = true;
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == trigger)
            col = false;
    }
}
