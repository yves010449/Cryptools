using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{

    public UnityEvent<Vector2> OnMove = new UnityEvent<Vector2>();
    Vector2 movement;

    private void Update() {
        getMovement();
    }

    private void getMovement() {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        OnMove?.Invoke(movement);
    }
}
