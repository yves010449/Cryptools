using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    public float speed = 5;

    public Vector3 moveDelta;
    private RaycastHit2D hit;

    public void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal")*speed;
        float y = Input.GetAxisRaw("Vertical")*speed;

        moveDelta = new Vector3(x, y, 0);

        hit = Physics2D.BoxCast(
            transform.position,
            boxCollider.size,
            0,
            new Vector2(0, moveDelta.y),
            Mathf.Abs(moveDelta.y * Time.deltaTime),
            LayerMask.GetMask("Creatures", "Blocking")
            );
        if (hit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(
            transform.position,
            boxCollider.size,
            0,
            new Vector2(moveDelta.x, 0),
            Mathf.Abs(moveDelta.x * Time.deltaTime),
            LayerMask.GetMask("Creatures", "Blocking")
            );
        if(hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
