using System.Collections;
using Platformer.Mechanics;
using UnityEngine;

public class BubbleCaptureEffect : MonoBehaviour
{
    [SerializeField] private SpriteRenderer bubbleGlow;
    [SerializeField] private SpriteRenderer enemyGlow;
    
    [Header("Capture Success")]
    [SerializeField] private float captureDuration;
    [SerializeField] private AnimationCurve positionCurve;
    [SerializeField] private AnimationCurve bubbleScaleCurve;
    [SerializeField] private AnimationCurve enemyScaleCurve;
    
    [Header("Capture Failed")]
    [SerializeField] private float rejectionDuration;
    [SerializeField] private AnimationCurve bubbleRejectionScaleCurve;
    [SerializeField] private AnimationCurve enemyRejectionScaleCurve;
    
    [SerializeField] private Material[] emissiveMaterialsByColor;

    public void PlayCaptureAnimation(EnemyController enemy, Vector3 bubbleCenter)
    {
        StartCoroutine(BubbleCaptureCoroutine(enemy, bubbleCenter, captureDuration));
    }

    public void PlayDenyAnimation(EnemyController enemy, Transform bubble)
    {
        StartCoroutine(BubbleDenyCoroutine(enemy, bubble, rejectionDuration));
    }

    private IEnumerator BubbleDenyCoroutine(EnemyController enemy, Transform bubble, float duration)
    {
        float startTime = Time.time;
        TypeRPS enemyColor = enemy.GetRPS();
        enemyGlow.sharedMaterial = emissiveMaterialsByColor[(int)enemyColor-1];
        bubbleGlow.transform.position = bubble.position;
        TypeRPS playerColor = GameController.Instance.model.player.GetRPS().Prev();
        bubbleGlow.sharedMaterial = emissiveMaterialsByColor[(int)playerColor-1];
        Vector3 enemyPosition = enemy.transform.position;
        Vector3 bubblePosition = bubble.position;
        if (duration <= 0) yield break;
        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            yield return null;
            bubbleGlow.transform.position = bubblePosition + Vector3.back;
            bubbleGlow.transform.localScale = bubbleRejectionScaleCurve.Evaluate(t) * Vector3.one;
            if (enemy != null)
            {
                enemyPosition = enemy.transform.position;
            }
            enemyGlow.transform.position = enemyPosition + Vector3.back;
            enemyGlow.transform.localScale = enemyRejectionScaleCurve.Evaluate(t) * Vector3.one;
        }
        Destroy(gameObject);
    }

    private IEnumerator BubbleCaptureCoroutine(EnemyController enemy, Vector3 bubbleCenter, float duration)
    {
        float startTime = Time.time;
        TypeRPS enemyColor = enemy.GetRPS();
        enemyGlow.sharedMaterial = emissiveMaterialsByColor[(int)enemyColor-1];
        bubbleGlow.transform.position = bubbleCenter;
        TypeRPS playerColor = GameController.Instance.model.player.GetRPS().Prev();
        bubbleGlow.sharedMaterial = emissiveMaterialsByColor[(int)playerColor-1];
        Vector3 enemyPosition = enemy.transform.position;
        if (duration <= 0) yield break;
        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            yield return null;
            Vector3 bubblePosition = Vector3.Lerp(bubbleCenter, enemyPosition, positionCurve.Evaluate(t));
            bubbleGlow.transform.position = bubblePosition + Vector3.back;
            bubbleGlow.transform.localScale = bubbleScaleCurve.Evaluate(t) * Vector3.one;
            enemyGlow.transform.position = enemyPosition + Vector3.back;
            enemyGlow.transform.localScale = enemyScaleCurve.Evaluate(t) * Vector3.one;
        }
        Destroy(gameObject);
    }
}
