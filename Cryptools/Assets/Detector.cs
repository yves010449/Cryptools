using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    private void Update() {
        
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("hit" + collision.gameObject.name);
        Debug.Log("sa");
    }
   
}
