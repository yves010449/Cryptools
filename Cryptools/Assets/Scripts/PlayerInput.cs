using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{

    public UnityEvent<Vector2> OnMove = new UnityEvent<Vector2>();
    public UnityEvent OnMouseClick = new UnityEvent();
    Vector2 movement;
    GameObject collision;


    private void Update() {
        getMovement();
        getInput();
    }

    private void getInput() {
        if (Input.GetMouseButtonDown(0)) {
            OnMouseClick?.Invoke();
        }
    }

    private void getMovement() {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        OnMove?.Invoke(movement);
    }
}
