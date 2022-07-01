using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{



    private void Update() {
        
    }

    public void getDetectedObject(GameObject resource) {

        if (resource.CompareTag("Rock") && resource != null) {
            resource.gameObject.GetComponent<Stone>().Hit();    
        }
    }
}
