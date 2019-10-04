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

    // Start is called before the first frame update
    void Start()
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
        playerCurrentHealth -= damageTaken;
        if (playerCurrentHealth <= 0)
        {
            SceneController.Lose();
        }
    }
   public static void HealthUp(float healAmount)
    {
        if (playerCurrentHealth < PlayerStats.playerMaxHealth)
        {
            playerCurrentHealth += healAmount;
        }
    }
}
