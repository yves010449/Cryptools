using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Movement movement;
    public AnimationController AnimController;
    private void Awake() {
        if(movement == null) {
            GetComponent<Movement>();
        }
        if (AnimController == null) {
            GetComponent<AnimationController>();
        }
    }
    public void HandleMovement(Vector2 movement) {
        this.movement.Move(movement);
    }
    public void HandleAnimation(Vector2 movement) {
        AnimController.Move(movement);
    }
}
