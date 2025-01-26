using UnityEngine;

public class DestroyPearl : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy this GameObject on collision
        Destroy(gameObject);
    }
    
}
