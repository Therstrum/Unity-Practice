using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMultiShooter : MonoBehaviour
{
    //health variables
    public float enemyHealth;
    public static float enemyMaxHealth;
    public static float enemyHealthAdjusted;

    //attack variables
    public bool enemyShotCooldown = true;
    float enemyCooldownSpeed = 2f;
    public float enemyCooldownTimer;

    Vector2 shotPos1;
    Vector2 shotPos2;
    Vector2 shotPos3;
    Vector2 shotPos4;
    Vector2 shotPos5;

    Vector2 lookDir1;
    Vector2 lookDir2;
    Vector2 lookDir3;
    Vector2 lookDir4;
    Vector2 lookDir5;

    Quaternion shotDir1;
    Quaternion shotDir2;
    Quaternion shotDir3;
    Quaternion shotDir4;
    Quaternion shotDir5;

    //Movement variables
    public bool tpCooldown = true;
    public float tpCooldownTime;
    public float tpLocation;
    Vector2 movement;


    //Gameplay Variables
    private float enemyDifficulty = 1;

    //Object properties
    public Rigidbody2D rb;
    public GameObject enemyWeapon;
    public GameObject credit;

    // Start is called before the first frame update
    void Awake()
    {
        WaveController.enemiesRemaining++;
        enemyMaxHealth = 30f;
        enemyHealthAdjusted = enemyMaxHealth *= .5f + ((WaveController.difficulty / 10f) * 5f);
        enemyHealth = enemyHealthAdjusted;

        movement.y = -.3f;
        //these should be arrays...
        shotPos1.Set((rb.position.x) - 2, (rb.position.y) - .5f);
        shotPos2.Set((rb.position.x) - 1, (rb.position.y) - 1);
        shotPos3.Set((rb.position.x), (rb.position.y) - 1.5f);
        shotPos4.Set((rb.position.x) + 1, (rb.position.y) -1f );
        shotPos5.Set((rb.position.x) + 2, (rb.position.y) - .5f);

        lookDir1 = (shotPos1 - rb.position).normalized;
        lookDir2 = (shotPos2 - rb.position).normalized;
        lookDir3 = (shotPos3 - rb.position).normalized;
        lookDir4 = (shotPos4 - rb.position).normalized;
        lookDir5 = (shotPos5 - rb.position).normalized;

        shotDir1.eulerAngles.Set(lookDir1.x, lookDir1.y,1);
        shotDir2.eulerAngles.Set(lookDir2.x, lookDir2.y, 1);
        shotDir3.eulerAngles.Set(lookDir3.x, lookDir3.y, 1);
        shotDir4.eulerAngles.Set(lookDir4.x, lookDir4.y, 1);
        shotDir5.eulerAngles.Set(lookDir5.x, lookDir5.y, 1);

    }

    // Update is called once per frame
    void Update()
    {
        shotPos1.Set((rb.position.x) - 2, (rb.position.y) - .5f);
        shotPos2.Set((rb.position.x) - 1, (rb.position.y) - 1);
        shotPos3.Set((rb.position.x), (rb.position.y) - 1.5f);
        shotPos4.Set((rb.position.x) + 1, (rb.position.y) - 1f);
        shotPos5.Set((rb.position.x) + 2, (rb.position.y) - .5f);

        lookDir1 = (shotPos1 - rb.position).normalized;
        lookDir2 = (shotPos2 - rb.position).normalized;
        lookDir3 = (shotPos3 - rb.position).normalized;
        lookDir4 = (shotPos4 - rb.position).normalized;
        lookDir5 = (shotPos5 - rb.position).normalized;

        shotDir1.eulerAngles.Set(lookDir1.x, lookDir1.y, 1);
        shotDir2.eulerAngles.Set(lookDir2.x, lookDir2.y, 1);
        shotDir3.eulerAngles.Set(lookDir3.x, lookDir3.y, 1);
        shotDir4.eulerAngles.Set(lookDir4.x, lookDir4.y, 1);
        shotDir5.eulerAngles.Set(lookDir5.x, lookDir5.y, 1);

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
        if (tpCooldown)
        {
            //set a random duration of next move
            tpCooldownTime -= Time.deltaTime;
            if (tpCooldownTime <= 0)
            {
                //choose left, right, or middle
                tpLocation = Random.Range(-9, 9);
                tpCooldown = false;
            }
        }
        else
        {
            //move in the chosen direction for the chosen duration
            rb.position= new Vector2(tpLocation, 3.25f);
            tpCooldownTime = Random.Range(3, 5);
            tpCooldown = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    { 
        Collider2D w = other.collider.GetComponent<CompositeCollider2D>();

            if (other.gameObject.tag == "playerWeapon")
        {
            ChangeHealth();
            Destroy(other.gameObject);
        }
    }
    public void ChangeHealth()
    {
        //public method that player weapons can call to damage this.
        enemyHealth -= PlayerStats.playerDamage;
        if (enemyHealth <= 0)
        {
            //DropLoot();
            WaveController.DropLoot(enemyDifficulty, rb.position);
            WaveController.enemiesRemaining--;
            Destroy(gameObject);
        }
    }

    public void Launch()
    {
        StartCoroutine("Launch2");
    }
    IEnumerator Launch2()
    {

        //animator.SetTrigger("fire");
        yield return new WaitForSeconds(0.9f);
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(.2f);
            GameObject enemyFire1 = Instantiate(enemyWeapon, shotPos1, shotDir1);
            GameObject enemyFire2 = Instantiate(enemyWeapon, shotPos2, shotDir2);
            GameObject enemyFire3 = Instantiate(enemyWeapon, shotPos3, shotDir3);
            GameObject enemyFire4 = Instantiate(enemyWeapon, shotPos4, shotDir4);
            GameObject enemyFire5 = Instantiate(enemyWeapon, shotPos5, shotDir5);

            EnemyShooterProjectile enemyShooterProjectile1 = enemyFire1.GetComponent<EnemyShooterProjectile>();
            EnemyShooterProjectile enemyShooterProjectile2 = enemyFire2.GetComponent<EnemyShooterProjectile>();
            EnemyShooterProjectile enemyShooterProjectile3 = enemyFire3.GetComponent<EnemyShooterProjectile>();
            EnemyShooterProjectile enemyShooterProjectile4 = enemyFire4.GetComponent<EnemyShooterProjectile>();
            EnemyShooterProjectile enemyShooterProjectile5 = enemyFire5.GetComponent<EnemyShooterProjectile>();

            enemyShooterProjectile1.Launch(lookDir1, 7f);
            enemyShooterProjectile2.Launch(lookDir2, 7f);
            enemyShooterProjectile3.Launch(lookDir3, 7f);
            enemyShooterProjectile4.Launch(lookDir4, 7f);
            enemyShooterProjectile5.Launch(lookDir5, 7f);
        }
        //animator.ResetTrigger("fire");

    }
}