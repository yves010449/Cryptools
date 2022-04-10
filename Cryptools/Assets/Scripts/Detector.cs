using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] private LayerMask Rock;
    public float range;


    



    private void Update() {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.down) * range, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), range, Rock);
        if (hit) {
            Debug.Log(hit.collider.gameObject.name);
            Destroy(hit.collider.gameObject);
        }
    }

    
   
}
