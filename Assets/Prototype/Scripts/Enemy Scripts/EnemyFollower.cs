using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollower : MonoBehaviour
{
    public bool isMoving = false;
    Vector2 getDirection;
    public Rigidbody2D rb;
    public float enemyHealth;
    static float enemyMaxHealth;
    public float enemyCooldownTimer;
    public float enemyCooldownSpeed;
    private float enemyDifficulty = 1;
    public GameObject credit;
    // Start is called before the first frame update
    void Awake()
    {
        WaveController.enemiesRemaining++;
        rb = GetComponent<Rigidbody2D>();
	    enemyMaxHealth = 40f;
        enemyHealth = enemyMaxHealth;
        enemyCooldownSpeed = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
    }


    void FixedUpdate()
    {

        if (enemyCooldownTimer > 0)
        {
            enemyCooldownTimer -= Time.fixedDeltaTime;

            if (enemyCooldownTimer <= 0)
            {
                isMoving = true;
            }
        }
        Vector2 lookDir = (playerController.playerPosition - rb.position).normalized;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg +90f;
        rb.rotation = angle;
        if (isMoving)
        {
            isMoving = false;
            rb.AddForce((playerController.playerPosition - rb.position).normalized * 15f, ForceMode2D.Impulse);
            enemyCooldownTimer = enemyCooldownSpeed;
            //enemyCooldownTimer = enemyCooldownSpeed;

        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "playerWeapon")
        {
            ChangeHealth();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag != "playerWeapon")
        {
            isMoving = false;
            //consider stopping movement too
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
}
