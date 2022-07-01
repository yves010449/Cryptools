using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBehavior : MonoBehaviour
{ 
    public void breakStone()
    {
        Destroy(gameObject);
    }
}
