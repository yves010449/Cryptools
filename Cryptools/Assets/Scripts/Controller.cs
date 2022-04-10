using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Movement movement;
    public AnimationController AnimController;
    public Interactor interactor;
    public Tools tool;

    private void Awake() {
        if(movement == null) {
            GetComponent<Movement>();
        }
        if (AnimController == null) {
            GetComponent<AnimationController>();
        }
        if (interactor == null) {
            GetComponent<Interactor>();
        }
        if(tool == null) {
            GetComponent<Tools>();
        }
    }
    public void HandleMovement(Vector2 movement) {
        this.movement.Move(movement);
    }
    public void HandleAnimation(Vector2 movement) {
        AnimController.Move(movement);
    }
    public void HandleRotation(Vector2 movement) {
        interactor.Rotate(movement);  
    }
}
