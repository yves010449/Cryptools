using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
   public Animator animator;
    Vector2 movement;
    private void Awake() {
        if(animator == null) {
            animator = GetComponent<Animator>();
        }
    }
    private void Update() {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        if(movement.x != 0 || movement.y !=0) {
            animator.SetBool("isMoving", true);
        }
        else {
            animator.SetBool("isMoving", false);
        }

    }
    public void Move(Vector2 movementInput) {
        this.movement = movementInput;
    }
}
