using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class credit : MonoBehaviour
{
    private Vector2 randomdir;
    Rigidbody2D rb;
    Vector2 distanceToPlayer;
    Vector2 movement;
    bool collecting = false;
    public static GameObject creditPrefab;
   


    void Awake()

    { 
        rb = GetComponent<Rigidbody2D>();
        //choose a random direction and move that way. Movement reduced by the drag of the rigidbody
        randomdir = new Vector2(Random.Range(-2.0f, 3f), Random.Range(-2, 3));
        rb.AddForce(randomdir *30f);
    }

    // Update is called once per frame
    void Update()
    {
        //if the player is in range
        if ((rb.position - playerController.playerPosition).magnitude <= 3)
        {
            //start the process of moving towards player
            collecting = true;
        }
        else
        {
            //otherwise do nothing
            collecting = false;
        }
    }
    private void FixedUpdate()
    {
        //if the player is in range
        if (collecting)
        {
            //get the direction to the player
            Vector2 direction = (playerController.playerPosition - rb.position).normalized;
            //move with force so there is still momentum if the condition becomes false
            //rb.AddForce(direction * 10f);

            //if needed, can move position instead
            Vector2 newPos = rb.position+ direction * Time.fixedDeltaTime * 6f;
            rb.MovePosition(newPos);
 
        }
        
    }
    //when colliding with a player. Add to total credits.
    public static void Collect()
    {
        {
            PlayerStats.credits++;
            Debug.Log("Credits: " + PlayerStats.credits);
        }
    }
}
