using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Movement movement;

    private void Awake() {
        if(movement == null) {
            GetComponent<Movement>();
        }
    }
    public void HandleMovement(Vector2 movement) {
        this.movement.Move(movement);
    }
}
