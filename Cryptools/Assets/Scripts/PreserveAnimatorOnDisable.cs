using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PreserveAnimatorOnDisable : MonoBehaviour
{

    private class AnimParam
    {
        public AnimatorControllerParameterType type;
        public string paramName;
        float hitpoints;

        public AnimParam(Animator anim, string paramName, AnimatorControllerParameterType type)
        {
            this.type = type;
            this.paramName = paramName;
            this.hitpoints = (float)anim.GetFloat(paramName);
        }
        public object GetHitPoints()
        {
            return hitpoints;
        }
    }

    public Animator anim;
    List<AnimParam> parms = new List<AnimParam>();

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        OnAnimDisable();
    }

    public void OnAnimDisable()
    {
        //Debug.Log("Saving Animator state: " + anim.parameters.Length);
        for (int i = 0; i < anim.parameters.Length; i++)
        {
            AnimatorControllerParameter p = anim.parameters[i];
            AnimParam ap = new AnimParam(anim, p.name, p.type);
            parms.Add(ap);
        }
    }

    void OnEnable()
    {
        //Debug.Log("Restoring Animator state.");
        foreach (AnimParam p in parms)
        {
            anim.SetFloat(p.paramName, (float)p.GetHitPoints());
        }
        parms.Clear();
    }
}