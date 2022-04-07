using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Transform interactor;
    Vector2 movementInput;


    

    public void Rotate(Vector2 movementInput) {
        this.movementInput = movementInput;
    }

    private void FixedUpdate() {
        if (movementInput.x > 0) {
            interactor.localRotation = Quaternion.Euler(0, 0, 90);
            interactor.localPosition = new Vector3((float)0.5, (float)-0.5, 0);
        }
        if (movementInput.x < 0) {
            interactor.localRotation = Quaternion.Euler(0, 0, -90);
            interactor.localPosition = new Vector3((float)-0.5, (float)-0.5, 0);
        }
        if (movementInput.y > 0) {
             interactor.localRotation = Quaternion.Euler(0, 0, 180);
            interactor.localPosition = new Vector3(0, 0, 0);
        }
        if (movementInput.y < 0) {
            interactor.localRotation = Quaternion.Euler(0, 0, 0);
            interactor.localPosition = new Vector3(0, -1, 0);
        }
    }
}
