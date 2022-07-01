using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBehavior : MonoBehaviour
{
    public Animator anim;

    private void Update()
    {

    }
    public void collect()
    {
        anim.SetFloat("HitPoints", anim.GetFloat("Hitpoints") - 1);
    }
}
