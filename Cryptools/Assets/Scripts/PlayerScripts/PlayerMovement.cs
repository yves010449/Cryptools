using Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator anim;

    public float rayCastX = 0f;
    public float rayCastY = 0f;

    Vector2 rayCastDirection;
    Vector2 movement;
    RaycastHit2D hit;

    public float attackRate = 3f;
    float nextAttackTime = 0f;

    [SerializeField]
    private AudioSource mine;

    [SerializeField]
    private AudioSource step;

    private void Start()
    {
        ////GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //GetComponent<Rigidbody2D>().angularVelocity = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (Input.GetMouseButtonDown(0))
            {
                hit = Physics2D.Raycast(transform.position + (transform.right * rayCastX) + (transform.up * rayCastY), rayCastDirection, .5f);
                Debug.Log(hit.transform.gameObject.name);
                if (hit.transform.gameObject.GetComponent<AIController>() == null)
                {
                    if (hit.transform.gameObject.GetComponent<ItemDropBehavior>().GetDrop().ItemImage.name == "Stone" &&
                        this.gameObject.GetComponent<InventoryController>().GetSelectedIndex() == 0)
                    {
                        anim.SetBool("IsMining", true);
                        Debug.Log("Hit!");
                    }
                    else if (hit.transform.gameObject.GetComponent<ItemDropBehavior>().GetDrop().ItemImage.name == "Wood" &&
                        this.gameObject.GetComponent<InventoryController>().GetSelectedIndex() == 1)
                    {
                        anim.SetBool("IsMining", true);
                        Debug.Log("Hit!");
                    }
                    else if (hit.transform.gameObject.GetComponent<ItemDropBehavior>().GetDrop().ItemImage.name == "Fiber")
                    {
                        anim.SetBool("IsMining", true);
                        Debug.Log("Hit!");
                    }

                    Debug.Log(hit.transform.gameObject.GetComponent<ItemDropBehavior>().GetDrop().ItemImage.name);
                }
                else
                {
                    if (Time.time >= nextAttackTime)
                    {
                        if (anim.GetFloat("Speed") == 0)
                            this.GetComponent<CreatureCombat>().Attack();
                        hit.transform.gameObject.GetComponent<CreatureStats>().TakeDamage(20);
                        nextAttackTime = Time.time + 1f / attackRate;
                    }                
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
        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    public void collect()
    {
        try
        {
            mine.Play();
            hit.transform.GetComponent<Animator>().SetFloat("HitPoint", hit.transform.GetComponent<Animator>().GetFloat("HitPoint") - 1f);
            hit.transform.GetComponent<Animator>().SetTrigger("Hit");

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

    public void Step()
    {
        step.Play();
    }

    void FixedUpdate()
    {
        if (anim.GetBool("IsAttacking") == false)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
