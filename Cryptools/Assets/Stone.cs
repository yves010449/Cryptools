using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public int requiredHits;
    private void Start() {
        requiredHits++;
    }

    public void Hit() {
        if(requiredHits < 0) {
            Destroy(gameObject,1f);
        }    
        requiredHits =- 1;
    }
}
