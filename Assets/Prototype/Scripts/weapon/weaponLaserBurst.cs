using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponLaserBurst : MonoBehaviour
{
    float damage;
    Rigidbody2D rigidbody2d;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        damage = 1 * PlayerStats.playerDamage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Collider2D w = other.collider.GetComponent<CompositeCollider2D>();

        if (w != null)
        {
            Destroy(gameObject);
        }
    }

}
