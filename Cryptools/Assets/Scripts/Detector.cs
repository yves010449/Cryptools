using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Detector : MonoBehaviour
{
    [SerializeField] private LayerMask Rock;
    public float range;

    public UnityEvent<GameObject> OnInteract = new UnityEvent<GameObject>();
   
    RaycastHit2D hit;
    Vector2 movementInput;


    public void Rotate(Vector2 movementInput) {
        this.movementInput = movementInput;
    }

    private void FixedUpdate() {
        if (movementInput.x > 0) {
            transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        if (movementInput.x < 0) {
            transform.localRotation = Quaternion.Euler(0, 0, -90);
        }
        if (movementInput.y > 0) {
            transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
        if (movementInput.y < 0) {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void Update() {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.down) * range, Color.red);
        hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), range, Rock);
    }

    public void DetectResource() {
        
        Debug.Log("clcik");
        
        if (hit) {
            Debug.Log(hit.collider.tag);
            OnInteract?.Invoke(hit.collider.gameObject);
        }
    }


   

    
   
}
