using System;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        } 
    }

    void Shoot()
    {
       // initialize the projectile on player.
       GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
       
       // Determine shooting direction.
       Vector2 direction = transform.localScale.x > 0 ? Vector2.left : Vector2.right;
       
       // set velocity 
       Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
       if (rb != null)
       {
           rb.velocity = direction * bulletSpeed;
       }
       
    }
}
