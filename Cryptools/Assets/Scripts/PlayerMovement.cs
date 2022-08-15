using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{

    public StoneBehavior stone;

    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator anim;

    public float rayCastX = 0f;
    public float rayCastY = 0f;

    Vector2 rayCastDirection;
    Vector2 movement;
    RaycastHit2D hit;

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (Input.GetMouseButtonDown(0))
            {

                hit = Physics2D.Raycast(transform.position + (transform.right * rayCastX) + (transform.up * rayCastY), rayCastDirection, .5f);
                if (hit)
                {
                    anim.SetBool("IsMining", true);
                    Debug.Log("Hit!");
                }
            }
            Debug.DrawRay(transform.position + (transform.right * rayCastX) + (transform.up * rayCastY), rayCastDirection * .5f, Color.red);
            if (Input.GetMouseButtonUp(0))
            {
                anim.SetBool("IsMining", false);
            }

            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);
            anim.SetFloat("Speed", movement.sqrMagnitude);

            if (movement.y == 1 && movement.x == 0)
            {
                anim.SetFloat("LastMoveY", 1);
                anim.SetFloat("LastMoveX", 0);
                rayCastDirection.x = 0;
                rayCastDirection.y = 1;
            }
            else if (movement.y == 0 && movement.x == 1)
            {
                anim.SetFloat("LastMoveY", 0);
                anim.SetFloat("LastMoveX", 1);
                rayCastDirection.x = 1;
                rayCastDirection.y = 0;
            }
            else if (movement.y == -1 && movement.x == 0)
            {
                anim.SetFloat("LastMoveY", -1);
                anim.SetFloat("LastMoveX", 0);
                rayCastDirection.x = 0;
                rayCastDirection.y = -1;
            }
            else if (movement.y == 0 && movement.x == -1)
            {
                anim.SetFloat("LastMoveY", 0);
                anim.SetFloat("LastMoveX", -1);
                rayCastDirection.x = -1;
                rayCastDirection.y = 0;
            }
        }
        catch (System.Exception)
        {

        }
       
    }
    public void collect()
    {
        try
        {
            hit.transform.GetComponent<Animator>().SetFloat("HitPoint", hit.transform.GetComponent<Animator>().GetFloat("HitPoint") - 1f);
        }
        catch (System.Exception)
        {
            anim.SetBool("IsMining", false);
        }
    }

    public void checkIfCollected()
    {
        if (hit.transform.GetComponent<Animator>().GetFloat("HitPoint") == 0)
            anim.SetBool("IsMining", false);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
