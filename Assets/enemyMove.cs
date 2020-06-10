using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMove : MonoBehaviour
{
    public float range = 5.0f;
    public float speed = 1.0f;

    private Vector2 pos1;
    private Vector2 pos2;

    private void Start() {
        pos1 = transform.position;
        pos2 = new Vector2(transform.position.x + range, transform.position.y);
    }

    void Update() {
        transform.position = Vector2.Lerp(pos1, pos2, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);
    }
}
