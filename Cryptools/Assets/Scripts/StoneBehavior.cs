using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBehavior : MonoBehaviour
{
    [SerializeField]
    Item stoneDrop;
    float spread = 2f;

    public void dropStone()
    {
        for (int i = 0; i < Random.Range(8, 15); i++)
        {
            Vector2 pos = this.transform.position;
            pos.x += spread * Random.value - spread / 2;
            pos.y += spread * Random.value - spread / 2;
            Instantiate(stoneDrop, pos, Quaternion.identity);
        }
        
    }

    public void breakStone()
    {
        Destroy(gameObject);
    }
}
