using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour
{
    public Transform Tool;
    Vector2 movementInput;




    public void Rotate(Vector2 movementInput) {
        this.movementInput = movementInput;
    }

    private void FixedUpdate() {
        if (movementInput.x > 0) {
           // Tool.localRotation = Quaternion.Euler(0, 0, 90);
            Tool.localPosition = new Vector3((float)0.5, (float)-0.5, 0);
        }
        if (movementInput.x < 0) {
          //  Tool.localRotation = Quaternion.Euler(0, 0, -90);
            Tool.localPosition = new Vector3((float)0.04, (float)-0.27, 0);
        }
        if (movementInput.y > 0) {
          //  Tool.localRotation = Quaternion.Euler(0, 0, 180);
            Tool.localPosition = new Vector3((float)-0.5, (float)-0.35, 0);
        }
        if (movementInput.y < 0) {
           // Tool.localRotation = Quaternion.Euler(0, 0, 0);
            Tool.localPosition = new Vector3((float) 0.1, 0, 0);
        }
    }
}
