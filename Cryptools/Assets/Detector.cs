using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    private void Update() {
        
    }
    
    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("hit" + collision.gameObject.name);
    }
}
