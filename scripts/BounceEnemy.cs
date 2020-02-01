using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceEnemy : Enemy
{


    private int currentKnockbackTimeout = 0;
    public int knockbackTimeout = 120;

    // Start is called before the first frame update
    new void Start()
    {
        base.Update();
    }

    // Update is called once per frame
    new void Update()
    {
        float xDiff = player.transform.position.x - transform.position.x;
        float yDiff = player.transform.position.y - transform.position.y;

        if (currentKnockbackTimeout != 0)
        {
            --currentKnockbackTimeout;
            xDiff = -xDiff;
        }

        FollowPlayer(xDiff, yDiff);
    }

    public new void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided in bounceEnemy");
        base.OnCollisionEnter2D(collision);
        if (collision.collider == base.player.GetComponent(typeof(Collider2D)))
        {
            ((PlayerHealth)player.GetComponent(typeof(PlayerHealth))).hitFor(10);
        }



        float xDir = player.transform.position.x - transform.position.x;
        float yDir = player.transform.position.y - transform.position.y;

        //bounce back a little bit
        Debug.Log("xDir: " + xDir + ", yDir: " + yDir);
        currentKnockbackTimeout = knockbackTimeout;
        //Vector2 newPos = rd.position + new Vector2(xDir * (-2), yDir * (-2));

        //animator.SetFloat("Horizontal", movement.x);
        //animator.SetFloat("Vertical", movement.y);
        //animator.SetFloat("Speed", movement.sqrMagnitude);

        //rd.AddForce(new Vector2(xDir* (-5f), yDir * (-5f)));
        //transform.position = newPos;

        Rigidbody2D playerRD = (Rigidbody2D)player.GetComponent(typeof(Rigidbody2D));

        //playerRD.AddForce(new Vector2(xDir * 2, yDir * 2));

        playerRD.MovePosition(playerRD.position + new Vector2(xDir, yDir) * 10);
    }
}
