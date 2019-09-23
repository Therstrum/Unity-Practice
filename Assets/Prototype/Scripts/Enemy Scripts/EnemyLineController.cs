using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLineController : MonoBehaviour
{
    public bool enemyShotCooldown = true;
    public float enemyCooldownSpeed = 2f;
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
        //MOVEMENT
        //if this isn't already moving
        if (rb.velocity.magnitude > 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
            enemyShotCooldown = true;            
        }
        if (enemyShotCooldown)
        {

            if (enemyCooldownTimer > 0)
            {
                enemyCooldownTimer -= Time.deltaTime;
            }
            else if (enemyCooldownTimer <= 0)
            {
                enemyShotCooldown = false;
                direction = new Vector2(Random.Range(-10, 10f), Random.Range(-10, 10));
                chosenDir = (rb.position - direction).normalized;
                enemyCooldownTimer = enemyCooldownSpeed;
            }
        }
    }


    void FixedUpdate()
    {
        if (!enemyShotCooldown && !isMoving)
        {
            rb.AddForce(chosenDir * 5f,ForceMode2D.Impulse);
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "HorizontalCollider")
        {


        }
        else if (other.gameObject.tag == "VerticalCollider")
        {


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

