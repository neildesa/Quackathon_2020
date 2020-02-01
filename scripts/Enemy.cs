using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed = 3f;

    public Animator animator;
    public Rigidbody2D rd;

    protected Vector2 moveDirection;

    protected Vector2 movement;

    // Start is called before the first frame update
    protected void Start()
    {
        
    }


    float getOneOrMinusOne(float f)
    {
        //float bottom = f == 0? 0.5f: f;
        return f== 0? 0: f / Mathf.Abs(f);
    }


    protected void FollowPlayer(float xDiff, float yDiff)
    {

        if (Mathf.Abs(xDiff) > Mathf.Abs(yDiff))
        {
            float ratio = xDiff / yDiff;

            movement.x = getOneOrMinusOne(xDiff);
            movement.y = movement.x / ratio;
        }
        else
        {
            float ratio = yDiff / xDiff;

            movement.y = getOneOrMinusOne(yDiff);
            movement.x = movement.y / ratio;
        }



        //a lot of this is taken from Finn's movement code
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }
    protected void Update()
    {

        moveDirection.x = player.transform.position.x - transform.position.x;
        moveDirection.y = player.transform.position.y - transform.position.y;
        FollowPlayer(moveDirection.x, moveDirection.y);
    }

    void FixedUpdate()
    {
        MoveWithMovement(movement);
    }

    protected void MoveWithMovement(Vector2 movement)
    {
        rd.MovePosition(rd.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == player.GetComponent(typeof(Collider2D)))
        {
            Debug.Log("collided with player");

        }
    }
}
