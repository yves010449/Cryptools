using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float detectionDistance;

    private float distance;
    public Animator anim;

    [SerializeField]
    public CreatureCombat combat;

    public float attackRate = 3f;
    float nextAttackTime = 0f;

    [SerializeField]
    AnimationClip attack;

    Vector2 movement;
    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        movement = new Vector2();
        //GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        ////GetComponent<Rigidbody2D>().angularVelocity = 0f;
        //GetComponent<Rigidbody2D>().useFullKinematicContacts = true;
        //GetComponent<Rigidbody2D>().AddForce(Vector2.up*5f);
        //Physics2D.IgnoreCollision(player.GetComponent<CapsuleCollider2D>(), this.GetComponent<CapsuleCollider2D>(), true);
    }

    private void Update()
    {
        if (direction == null)
            return;

        //if (direction.x > 0)
        //    movement.x = 1;
        //else if (direction.x < 0)
        //    movement.x = -1;

        //if (direction.y > 0)
        //    movement.y = 1;
        //else if (direction.y > 0)
        //    movement.y = -1;

        //if (speed == 0)
        //{
        //    movement.x = 0;
        //    movement.y = 0;
        //}

        anim.SetFloat("Horizontal", direction.x);
        anim.SetFloat("Vertical", direction.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        distance = Vector2.Distance(transform.position, player.transform.position);
        direction = player.transform.position - transform.position;

        //Debug.Log(direction);

        if (distance < detectionDistance)
        {
            if (speed > 0)
                anim.SetBool("IsMoving", true);
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
    }

    float tempSpeed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        tempSpeed = speed;
        //Debug.Log("cOLL");
        if (collision.gameObject.name == "Player")
        {
            anim.SetBool("IsMoving", false);
            speed = 0;
            StartCoroutine(Attack(collision));
        }
    }

    IEnumerator Attack(Collision2D collision)
    {
        //if (Time.time >= nextAttackTime)
        //{
        //    if (collision.gameObject.name == "Player")
        //    {
        //        combat.Attack();
        //        nextAttackTime = Time.time + 1f / attackRate;
        //        Debug.Log("Attacking!");
        //    }
        //}

        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(10);
            combat.Attack();
        }
        yield return new WaitForSeconds(attack.length);
        StartCoroutine(Attack(collision));
    }

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    Debug.Log("Staying!");
    //    if (Time.time >= nextAttackTime)
    //    {
    //        if (collision.gameObject.name == "Player")
    //        {
    //            combat.Attack();
    //            nextAttackTime = Time.time + 1f / attackRate;
    //        }
    //    }

    //}

    private void OnCollisionExit2D(Collision2D collision)
    {
        speed = tempSpeed;
        StopAllCoroutines();
    }

}
