using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bubblePrefab;
    public Transform firePoint;
    public float bubbleSpeed = 3f;
    public float upwardForce = 2f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        } 
    }

    void Shoot()
    {
        //Spawn bubble.
        GameObject bubble = Instantiate(bubblePrefab, firePoint.position, Quaternion.identity);
        
        //Determine the direction based on the players direction.
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        
        // Add a slightly upward movement to the velocity.
        Vector2 bubbleVelocity = (direction * bubbleSpeed) + (Vector2.up * upwardForce);
        
        // Add randomness to make it feel more natural.
        bubbleVelocity += new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0, 0.5f));
        
        // Apply the velocity to the RigidBody2D
        Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = bubbleVelocity;
        }
    }
}
