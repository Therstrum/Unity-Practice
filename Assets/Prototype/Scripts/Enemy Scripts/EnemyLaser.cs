using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    //health variables
    public float enemyHealth;
    public static float enemyMaxHealth;
    public static float enemyHealthAdjusted;

    //attack variables
    public bool enemyShotCooldown = true;
    bool isShooting = true;
    float enemyCooldownSpeed = 6f;
    public float enemyCooldownTimer;  
    public Vector2 shotDirection;
    int damage = 10;

    //Gameplay Variables
    private float enemyDifficulty = 1;

    //Attached GameObject properties
    public Rigidbody2D rb;
    public GameObject enemyWeapon;
    public GameObject credit;
    public LineRenderer traceShot;
    public LineRenderer bigLaser;

    //Movement Variables
    Vector2 movement;
    bool isStrafing = false;
    public float strafingTime;
    public int movementDir;
    private Vector2 currentPosition;

    // Start is called before the first frame update

    void Awake()
    {
        WaveController.enemiesRemaining++;
        float pickSide = Random.Range(0, 101);
        if (pickSide <= 50)
        {
            rb.position = new Vector2(-8.2f, 5);
        }
        else
        {
            rb.position = new Vector2(8.2f, 5);
        }
        enemyMaxHealth = 30f;
        enemyHealthAdjusted = enemyMaxHealth *= .5f + ((WaveController.difficulty / 10f) * 5f);
        enemyHealth = enemyHealthAdjusted;

        shotDirection.x = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyShotCooldown)
        {//if on a shot cooldown subtract from the cooldown timer, 
            enemyCooldownTimer -= Time.deltaTime;
            //when the cooldown timer finishes, cooldown is off
            if (enemyCooldownTimer <= 0)
            {
                enemyShotCooldown = false;
                isShooting = true;
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
        if (!isStrafing)
        {
            //set a random duration of next move
            strafingTime = Random.Range(1, 4);
            //choose left, right, or middle
            float moveDir = Random.Range(0, 100);
            if (moveDir <= 50)
            {
                movementDir = -1;
            }
            else
            {
                movementDir = 1;
            }
            isStrafing = true;
        }
        else if (!isShooting)
        {
            //move in the chosen direction for the chosen duration
            movement.y = movementDir * 4f;
            strafingTime -= Time.deltaTime;
            //when duration elapses, stop moving and go back to top.
            if (strafingTime <= 0)
            {
                isStrafing = false;
            }
            rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Collider2D w = other.collider.GetComponent<CompositeCollider2D>();
        if (w != null)
        {
            movementDir = movementDir * -1;
        }
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
        shotDirection.x = 0f;
        shotDirection.y = Random.Range(-7, 7);
        Vector2 laserDirection = (shotDirection - rb.position).normalized;
        float lookDirection = Mathf.Atan2(laserDirection.y, laserDirection.x) * Mathf.Rad2Deg + 90f;
        rb.rotation = lookDirection;
        RaycastHit2D hitInfo = Physics2D.Raycast(rb.position, laserDirection*100f);
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 3; i++)
        {
            if (hitInfo)
            {
                traceShot.SetPosition(0, this.rb.position);
                traceShot.SetPosition(1, this.rb.position + laserDirection * 100f);

            }
            else
            {
                traceShot.SetPosition(0, this.rb.position);
                traceShot.SetPosition(1, this.rb.position+ laserDirection *100f);
            }
            Debug.DrawRay(this.rb.position, laserDirection*100f);
            traceShot.enabled = true;
            yield return new WaitForSeconds(.02f);
            traceShot.enabled = false;
            yield return new WaitForSeconds(.15f);
        }
        yield return new WaitForSeconds(.5f);

        
        if (hitInfo)
        {
            bigLaser.SetPosition(0, this.rb.position);
            bigLaser.SetPosition(1, this.rb.position + laserDirection * 100f);
            playerController player = hitInfo.transform.GetComponent<playerController>();
            if (player != null)
            {
                /*while (bigLaser.enabled == true);
                {
                    playerController.ChangeHealth(damage);
                }
                */
                
            }
        }
        bigLaser.enabled = true;
        yield return new WaitForSeconds(3f);
        bigLaser.enabled = false;
        isShooting = false;
    }
}
//use this for final laser
/*      

                if (hitInfo)
            {
                traceShot.SetPosition(0, this.rb.position);
                traceShot.SetPosition(1, hitInfo.point);
                            if (player != null)
            {
                playerController player = hitInfo.transform.GetComponent<playerController>();
                //playerController.ChangeHealth(damage);
            }

            }
            else
            {
                traceShot.SetPosition(0, this.rb.position);
                traceShot.SetPosition(1, this.rb.position + shotDirection * 100f);
            }
            }
    */
