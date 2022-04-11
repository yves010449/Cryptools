using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour {
    public Animator animator;
    public int requiredHits;

    private void Awake() {
        if (animator == null) {
            animator = GetComponent<Animator>();
        }
        animator.SetInteger("Hits", requiredHits);
    }

    private void Start() {
        Debug.Log(requiredHits);
    }

    public void Hit() {

        animator.SetInteger("Hits", requiredHits);
        Debug.Log(requiredHits);
        requiredHits--;
    }
    public void DestroyRock() {
        Destroy(gameObject);
    }
}
