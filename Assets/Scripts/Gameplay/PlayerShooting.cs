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
       // initialize the projectile.
       GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
       
       // set velocity 
       Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
       if (rb != null)
       {
           rb.velocity = firePoint.right * bulletSpeed;
       }
       
    }
}
