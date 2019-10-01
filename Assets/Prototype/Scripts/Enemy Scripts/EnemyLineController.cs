using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLineController : MonoBehaviour
{
    public bool enemyShotCooldown = true;
    public float enemyCooldownSpeed = 3f;
    public float enemyCooldownTimer;
    public GameObject enemyWeapon;
    public Rigidbody2D rb;
    public float enemyHealth;
    public static float enemyMaxHealth;
    public static float enemyHealthAdjusted;
    Vector2 movement;
    Vector2 chosenDir;
    public bool isMoving = false;
    public int movementDir;
    private Vector2 currentPosition;
    public Vector2 direction;
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
         
        if (enemyCooldownTimer > 0)
        {
            enemyCooldownTimer -= Time.fixedDeltaTime;

            if (enemyCooldownTimer <= 0)
            {
                isMoving = true;
            }
        }
    }


    void FixedUpdate()
    {
        Vector2 lookDir = (playerController.playerPosition - rb.position).normalized;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg-90f;
        rb.rotation = angle;
        if (isMoving)
        {
            rb.AddForce((playerController.playerPosition - rb.position).normalized * 5f, ForceMode2D.Impulse);
            isMoving = false;
            enemyCooldownTimer = enemyCooldownSpeed;
        }
    }


    public void ChangeHealth(float damage)
    {
        //public method that player weapons can call to damage this.
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            //DropLoot();
            WaveController.DropLoot(enemyDifficulty, rb.position);
            dead = true;
            WaveController.enemiesRemaining--;
            Destroy(gameObject);
        }
    }
}

