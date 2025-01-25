using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bubblePrefab;
    public float bubbleSpeed = 3f;
    public float upwardForce = 2f;
    public float firePointOffset = 1.5f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        } 
    }

    void Shoot()
    {
        //Determine the direction based on the players direction.
        Debug.Log(GetComponent<SpriteRenderer>().flipX);
        Vector2 direction = !GetComponent<SpriteRenderer>().flipX ? Vector2.right : Vector2.left;
        
        //Spawn bubble.
        Vector3 offset = new Vector3(direction.x * firePointOffset, 0, 0); 
        GameObject bubble = Instantiate(bubblePrefab, transform.position + offset, Quaternion.identity);
        
        // Add a slightly upward movement to the velocity.
        Vector2 bubbleVelocity = (direction * bubbleSpeed) + (Vector2.up * upwardForce);
        
        // Add randomness to make it feel more natural.
        bubbleVelocity += new Vector2(0, Random.Range(0, 0.5f));
        
        // Apply the velocity to the RigidBody2D
        Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = bubbleVelocity;
        }
    }
}
