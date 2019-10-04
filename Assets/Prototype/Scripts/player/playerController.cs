using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    //health
    public static float playerCurrentHealth =100;
    //movement
    public Rigidbody2D playerRigidBody;
    Vector2 movement;
    public Animator playerAnimator;
    public static Vector2 playerPosition;
    


    //weapons
    public GameObject laserBurstProjectile;
    bool shotCooldown = false;
    float shotCooldownTimer;

    static bool invincible = false;
    [SerializeField]
    public static float invincibleTime;
    static bool playerHit = false;

    // Start is called before the first frame update
    void Awake()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //movement controls
        playerPosition = playerRigidBody.position;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        playerAnimator.SetFloat("Horizontal", movement.x);
        playerAnimator.SetFloat("Speed", movement.sqrMagnitude);
        //weapons
        if (Input.GetKey(KeyCode.Space))
        {
            Launch();
        }
        if (shotCooldown)
        {
            shotCooldownTimer -= Time.deltaTime;
            if (shotCooldownTimer < 0)
                shotCooldown = false;
        }
        if (invincibleTime >= 0)
        {
            invincibleTime -= Time.deltaTime;
            if (invincibleTime < 0)
            {
                invincible = false;
            }
        }
        if (playerHit)
        {
            playerHit = false;
            StartCoroutine("Flash");
        }
    }

    private void FixedUpdate()
    {
        playerRigidBody.MovePosition(playerRigidBody.position + movement * PlayerStats.playerSpeed * Time.fixedDeltaTime);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Credit")
        {
            credit.Collect();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "enemy" || collision.gameObject.tag == "Laser")
        {
            ChangeHealth(10f);
        }


    }
    //methods
    void Launch()
    {
        //weaponLaserBurst
        if (shotCooldown)
        {
            return;
        }
        else
        {
            GameObject laserBurst = Instantiate(laserBurstProjectile, playerRigidBody.position + Vector2.up * 1f, Quaternion.identity);

            weaponLaserBurst weaponLaserBurst = laserBurst.GetComponent<weaponLaserBurst>();
            weaponLaserBurst.Launch(Vector2.up, PlayerStats.playerShotSpeed);
            shotCooldown = true;
            shotCooldownTimer = PlayerStats.playerShotRate;
        }
    }

    public static void ChangeHealth(float damageTaken)
    {
        if (!invincible)
            
        {
            playerHit = true;
            invincible = true;
            playerCurrentHealth -= damageTaken;
            if (playerCurrentHealth <= 0)
            {
                SceneController.Lose();
            }
            invincibleTime = 1.25f;
        }

    }
   public static void HealthUp(float healAmount)
    {
        if (playerCurrentHealth < PlayerStats.playerMaxHealth)
        {
            playerCurrentHealth += healAmount;
        }
    }
    IEnumerator Flash()
    {
        Renderer ren = GetComponent<SpriteRenderer>();
        for (int i = 0; i < 6; i++)
        {
            ren.enabled = false;
            yield return new WaitForSeconds(.1f);
            ren.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
    }
}
