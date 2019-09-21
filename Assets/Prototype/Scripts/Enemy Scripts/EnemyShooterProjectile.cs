using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterProjectile : MonoBehaviour
{
    float damage = 10f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Launch(Vector2 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Collider2D e = other.collider.GetComponent<CompositeCollider2D>();
        GameObject p = other.gameObject;
        if (e != null)
        {
            Destroy(gameObject);
        }
        if (p.name == "PlayerCharacter")
        {
            playerController.ChangeHealth(damage);
            Destroy(gameObject);
        }
    }
}
