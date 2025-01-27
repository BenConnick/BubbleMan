using System;
using Platformer.Mechanics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerShooting : MonoBehaviour
{
    private static readonly int ShootProp = Animator.StringToHash("shoot");
    public GameObject bubblePrefab;
    public float bubbleSpeed = 3f;
    public float upwardForce = 2f;
    public float firePointOffset = 1.5f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            
            // shoot anim
            GetComponent<Animator>().SetTrigger(ShootProp);
        } 
    }

    void Shoot()
    {
        //Determine the direction based on the players direction.
        Vector2 direction = !GetComponent<SpriteRenderer>().flipX ? Vector2.right : Vector2.left;
        
        //Spawn bubble.
        Vector3 offset = new Vector3(direction.x * firePointOffset, 0, 0); 
        GameObject bubble = Instantiate(bubblePrefab, transform.position + offset, Quaternion.identity);
        
        // Add a slightly upward movement to the velocity.
        Vector2 bubbleVelocity = (direction * bubbleSpeed) + (Vector2.up * upwardForce);
        
        // Apply the velocity to the RigidBody2D
        Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>();

        TypeRPS playerPowerType = GetComponent<RockPaperScissorsComponent>().Value;
        var typeRPSComp = bubble.GetComponent<RockPaperScissorsComponent>();
        if (typeRPSComp != null)
        {
            typeRPSComp.Value = playerPowerType;
        }
        
        var spriteRenderer = bubble.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = playerPowerType.Prev().GetHalfColor();
        }

        if (rb != null)
        {
            rb.velocity = bubbleVelocity;
        }
        
        // limit total bubbles
        bubble.GetComponent<BubbleProjectile>().Register();
    }
}
