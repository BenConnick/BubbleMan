using System;
using System.Collections;
using Platformer.Core;
using Platformer.Gameplay;
using Platformer.Mechanics;
using UnityEngine;

public class BubbleProjectile : MonoBehaviour
{
    public GameObject PopVFXPrefab;
    public GameObject EnemyPickupPrefab;

    public float FloatForce;
    
    private Rigidbody2D rb2d;

    public float SpawnAnimationDuration = .2f;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(PlaySpawnAnimation());
    }

    private IEnumerator PlaySpawnAnimation()
    {
        float startTime = Time.time;
        float t = 0;
        if (SpawnAnimationDuration == 0) yield break;
        while (t < 1)
        {
            t = (Time.time - startTime) / SpawnAnimationDuration;
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
            yield return null;
        }
        transform.localScale = Vector3.one;
    }

    public void FixedUpdate()
    {
        if (rb2d == null) return;
        if (FloatForce == 0) return;
        rb2d.AddForce(new Vector2(0, FloatForce), ForceMode2D.Force);
    }

    public void OnDestroy()
    {
        if (Application.isPlaying == false) return;
        SpawnPop();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.collider.GetComponentInChildren<EnemyController>();
        if (enemy != null)
        {
            Destroy(enemy.gameObject);
            var pickup = Instantiate(EnemyPickupPrefab, enemy.transform.position, Quaternion.identity);
            pickup.GetComponent<EnemyPickup>().SetEnemy(enemy.gameObject);
            Destroy(gameObject);
        }
    }

    private void SpawnPop()
    {
        if (PopVFXPrefab == null) return;
        Instantiate(PopVFXPrefab, transform.position, Quaternion.identity);
    }
}
