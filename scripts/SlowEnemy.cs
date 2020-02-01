using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEnemy : Enemy
{
    // Start is called before the first frame update

    private enum State { Moving, ChargingUp, Hitting}

    private State currentState = State.Moving;

    public float WalkingSpeed = 3f;
    public float ChargingUpSpeed = 0f;
    private float HittingSpeed = 50f;
    private int currentChargeTime;
    public int ChargeUpTime = 60;
    public int HitStrength = 100;
    private int hitTimeMax = 20;
    private int currentHitTime = 0;

    private Vector2 lastMoveDirection;

    new void Start()
    {
        base.Update();
    }

    // Update is called once per frame
    new void Update()
    {
        switch (currentState)
        {
            case State.Moving:
                base.moveSpeed = WalkingSpeed;
                base.Update();
                break;
            case State.ChargingUp:
                base.moveSpeed = ChargingUpSpeed;
                --currentChargeTime;
                if (currentChargeTime <= 0)
                {
                    currentState = State.Hitting;
                    currentHitTime = hitTimeMax;
                }
                base.Update();
                break;
            case State.Hitting:
                base.movement = lastMoveDirection;
                base.moveDirection = lastMoveDirection;
                base.moveSpeed = HittingSpeed;

                --currentHitTime;
                if (currentHitTime == 0)
                    currentState = State.Moving;
                base.MoveWithMovement(lastMoveDirection);
                //base.Update();
                break;
        }
        //FollowPlayer(lastMoveDirection.x, lastMoveDirection.y);
        //MoveWithMovement(lastMoveDirection);
    }

    public new void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == base.player.GetComponent(typeof(Collider2D)))
        {
            Debug.Log("collided in slowEnemy");
            base.OnCollisionEnter2D(collision);
            switch (currentState)
            {
                case State.Moving:
                    currentState = State.ChargingUp;
                    currentChargeTime = ChargeUpTime;
                    lastMoveDirection = moveDirection;
                    break;
                case State.ChargingUp:
                    break;
                case State.Hitting:

                    PlayerHealth ph = (PlayerHealth)player.GetComponent(typeof(PlayerHealth));
                    ph.hitFor(HitStrength); //take almost all health but 1;

                    break;


            }
        }
    }
}
