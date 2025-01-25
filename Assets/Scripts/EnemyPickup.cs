using System.Collections;
using System.Collections.Generic;
using Platformer.Mechanics;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(RockPaperScissorsComponent))]
public class EnemyPickup : MonoBehaviour
{
    public SpriteRenderer InnerRenderer;
    public RockPaperScissorsComponent Type;
    
    public AnimationCurve Curve;
    public float SpawnAnimationDuration;
    public float EndScale = 1.5f;
    
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
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Curve.Evaluate(t)) * EndScale;
            yield return null;
        }
        transform.localScale = Vector3.one * EndScale;
    }
    
    public void SetEnemy(GameObject enemyObject)
    {
        var targetRenderer = enemyObject.GetComponentInChildren<SpriteRenderer>();
        if (targetRenderer != null)
        {
            InnerRenderer.sprite = targetRenderer.sprite;
            InnerRenderer.color = targetRenderer.color;
        }

        var typeHolder = enemyObject.GetComponentInChildren<RockPaperScissorsComponent>();
        if (typeHolder != null)
        {
            Type.Value = typeHolder.Value;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject); 
            
        }
    }
}
