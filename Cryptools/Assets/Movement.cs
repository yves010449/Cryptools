using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Vector2 movement;
    Rigidbody2D rb;
    public float speed = 10;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 movementInput) {
        this.movement = movementInput;
    }
    private void FixedUpdate() {
        rb.velocity =  speed * movement * Time.fixedDeltaTime;
    }
}
