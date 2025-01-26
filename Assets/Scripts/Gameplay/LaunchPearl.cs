using UnityEngine;

public class LaunchPearl : MonoBehaviour
{ 
    public GameObject prefab;
    public Transform launchPoint;
    public float launchForce = 10f;
    public Vector2 launchDirection = new Vector2(1, 1);
    public Vector2 offset = new Vector2(0, 1);  // Offset above the enemy
    
    
    
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            Launch();
        }
    }

    void Launch()
    {
        if (prefab != null && launchPoint != null)
        {
            // Apply the offset to the launch position
            Vector2 spawnPosition = (Vector2)launchPoint.position + offset;

            GameObject instance = Instantiate(prefab, spawnPosition, launchPoint.rotation);

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(launchDirection.normalized * launchForce, ForceMode2D.Impulse);
            }
        }
        else
        {
            Debug.LogError("Launch Pearl needs a launch point and prefab assigned.");
        }
    }
}
