using UnityEngine;

public class LaunchPearl : MonoBehaviour
{ 
    public GameObject prefab;
    public Transform launchPoint;
    public float launchForce = 10f;
    public Vector2 launchDirection = new Vector2(1, 1);
    
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Launch();
        }
    }

    void Launch()
    {
        if (prefab != null && launchPoint != null)
        {
            GameObject instance = Instantiate(prefab, launchPoint.position, launchPoint.rotation);
            
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(launchDirection.normalized * launchForce, ForceMode2D.Impulse);
            }
        }
        else
        {
            Debug.LogError("Launch Pearl needs a launch point");
        }
    }
}
