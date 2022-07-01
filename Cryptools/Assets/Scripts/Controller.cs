using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Movement movement;
    public AnimationController AnimController;
    public Interactor interactor;
    public Detector detector;

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
        if(detector == null) {
            GetComponent<Detector>();
        }
    }
    public void HandleMovement(Vector2 movement) {
        this.movement.Move(movement);
    }
    public void HandleAnimation(Vector2 movement) {
        AnimController.Move(movement);
    }
    public void HandleRotation(Vector2 movement) {
        detector.Rotate(movement);  
    }
    public void HandleDetector() {
        detector.DetectResource();
    } 
    
}
