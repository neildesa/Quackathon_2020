using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //public int health = 100;
    public int Health { get; protected set; } = 100;

    public void hitFor(int hits)
    {
        Health -= hits;
        checkDeath();
    } 
    public void healFor(int points)
    {
        Health += points;
        checkDeath();
    }

    protected void checkDeath()
    {
        if (Health <= 0)
        {
            Debug.Log("player has no health, dying.");
            this.gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
