using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullPoints : MonoBehaviour
   
{
    float healAmount = 10f;
    public Rigidbody2D rb;
    Vector2 origin;
    

    // Start is called before the first frame update
    void Awake()
    {
        origin.x = 0;
        origin.y = 0;
        Vector2 moveDir = (rb.position - origin).normalized;
        rb.AddForce(moveDir * 1.5f,ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            if (playerController.playerCurrentHealth < PlayerStats.playerMaxHealth)
            {
                {
                    playerController.HealthUp(healAmount);
                    Destroy(gameObject);
                }
            }
    }
}
