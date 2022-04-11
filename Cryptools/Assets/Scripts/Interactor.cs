using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{



    private void Update() {
        
    }

    public void getDetectedObject(GameObject resource) {
        if (resource.CompareTag("Rock") && resource != null) {
            Debug.Log("Mining my own business");
            Destroy(resource, 2.2f);
        }
    }






}
