﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShooterController : MonoBehaviour
{
    public bool enemyShotCooldown = true;
    float enemyCooldownSpeed = 2f;
    public float enemyCooldownTimer;
    public GameObject enemyWeapon;
    public Rigidbody2D rb;
    public float enemyHealth;
    public static float enemyMaxHealth;
    public static float enemyHealthAdjusted;
    Vector2 movement;
    bool isStrafing = false;
    public float strafingTime;
    public int movementDir;
    private Vector2 currentPosition;
    Animator animator;
    private float enemyDifficulty = 1;

    public GameObject credit;
    public bool dead = false;

    // Start is called before the first frame update
    void Awake()
    {
        enemyMaxHealth = 30f;
        enemyHealthAdjusted = enemyMaxHealth *= .5f + ((WaveController.difficulty / 10f) * 5f);
        enemyHealth = enemyHealthAdjusted;
        WaveController.enemiesRemaining++;
        enemyCooldownTimer = 1f + Random.Range(.75f, 1.5f);
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //MOVEMENT
        //if this isn't already moving
        if (!isStrafing)
        {
            //set a random duration of next move
            strafingTime = Random.Range(1, 4);
            //choose left, right, or middle
            movementDir = Random.Range(-1, 2);
            isStrafing = true;
        }
        else
        {
            //move in the chosen direction for the chosen duration
            movement.x = movementDir * 2f;
            strafingTime -= Time.deltaTime;
            //when duration elapses, stop moving and go back to top.
            if (strafingTime <= 0)
            {
                isStrafing = false;
            }
        }
        //constantly falling
        movement.y = -0.2f;

        //SHOOTING
        if (enemyShotCooldown)
        {//if on a shot cooldown subtract from the cooldown timer, 
            enemyCooldownTimer -= Time.deltaTime;
            //when the cooldown timer finishes, cooldown is off
            if (enemyCooldownTimer <= 0)
            {
                enemyShotCooldown = false;
            }
        }
        else
        //if not on shot cooldown, start a fire. Go back on cooldown and reset the cooldown timer.
        {
            enemyShotCooldown = true;
            enemyCooldownTimer = enemyCooldownSpeed + Random.Range(.2f, 2);
            Launch();
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        //when the enemy gets to the bottom of the screen, teleport back to top.
        Collider2D w = other.collider.GetComponent<CompositeCollider2D>();
        movementDir = movementDir * -1;
        if (other.gameObject.tag == "Teleportback")
        {
            ReturnToTop(rb.position.y);   
        }

    }

    public void ChangeHealth(float damage)
    {
        //public method that player weapons can call to damage this.
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            //DropLoot();
            WaveController.DropLoot(credit, enemyDifficulty, rb.position);
            dead = true;
            WaveController.enemiesRemaining--;
            Destroy(gameObject);
        }
    }
    private void ReturnToTop (float y)
    {
        //not sure if 'this' is necessary
        this.rb.position = this.rb.position + 11f * Vector2.up;
    }

   /* private void DropLoot()
    {
        this.lootDropped = Random.Range(1 * PlayerStats.lootChance, 100);
        this.lootAmount = Random.Range(5 * PlayerStats.lootMulti, 20 * PlayerStats.lootMulti);
        Debug.Log("loot chance:" + lootDropped + "-- loot amount: " + lootAmount);
        if (lootDropped >= 40)
        {
            for (int i = 1; i < this.lootAmount; i++)
            {
                int l = i;
                Vector2 randomize = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
                GameObject drop = Instantiate(credit, rb.position + randomize, Quaternion.identity);
            }
        }
        
        lootAmount = 0f;
        lootDropped = 0f;
        return;
          
    }
    */
    public void Launch()
    {
        StartCoroutine("Launch2");
    }
    IEnumerator Launch2()
    {
        
        animator.SetTrigger("fire");
        yield return new WaitForSeconds(0.9f);
        GameObject enemyFire = Instantiate(enemyWeapon, rb.position * 1.0f, Quaternion.identity);
        EnemyShooterProjectile enemyShooterProjectile = enemyFire.GetComponent<EnemyShooterProjectile>();
        enemyShooterProjectile.Launch((playerController.playerPosition - rb.position).normalized, 12f);
        animator.ResetTrigger("fire");
        
    }
}

