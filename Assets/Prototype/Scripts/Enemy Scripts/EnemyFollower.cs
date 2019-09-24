using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollower : MonoBehaviour
{
    bool isMoving = false;
    Vector2 getDirection;
    public RigidBody2d rb;
    static float enemyHealth;
    static float enemyMaxHealth;
    static float enemyHealthAdjusted;
    // Start is called before the first frame update
    void Awake()
    {
        rb = RigidBody2d.GetComponent<RigidBody2d>();
	 enemyMaxHealth = 30f;
        enemyHealthAdjusted = enemyMaxHealth *= .5f + ((WaveController.difficulty / 10f) * 5f);
        enemyHealth = enemyHealthAdjusted;
        WaveController.enemiesRemaining++;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
	{
	    //rotate the sprite towards the player
	    //rb.transform.z = getDirection;

	    //get the vector towards the player and start movement
	    getDirection = (playerController.playerPosition - rb.position).normalized
	    StartCoroutine(Movement(getDirection));
    }
    void FixedUpdate()
    {
	
    }
    void OnCollisionEnter2d(Collision other)
	{
	    if (other.GameObject.Tag != “Projectile”)
	    {
	     isMoving = false;
	     //consider stopping movement too
	    }
	}
    IEnumerator Movement(Vector2 direction)
    {
	isMoving = true;
	;
	yield return new WaitForSeconds(2);
	rb.AddForce(direction * 2f,ForceMode2d.Impulse);
	yield return new WaitForSeconds(2);
	isMoving = false;	
    }

}
