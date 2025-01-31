using System;
using System.Collections;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Gameplay;
using Platformer.Mechanics;
using UnityEngine;

public class BubbleProjectile : MonoBehaviour
{
    public GameObject PopVFXPrefab;
    public BubbleCaptureEffect CaptureFXPrefab;
    public GameObject EnemyPickupPrefab;
    public int MaxBubbles = 10;
    
    private static readonly List<BubbleProjectile> _Instances = new List<BubbleProjectile>();
    
    private BubbleCaptureEffect _ActiveDenyEffect;

    public void Register()
    {
        _Instances.Add(this);
        
        // limit total bubbles
        if (_Instances.Count > MaxBubbles)
        {
            Destroy(_Instances[0].gameObject);
        }
    }

    public float FloatForce;
    
    private Rigidbody2D rb2d;

    public float SpawnAnimationDuration = .2f;
    public AnimationCurve SpawnAnimationCurve;

    public AudioSource Sounds;
    public AudioClip SpawnSound;

    private bool _Quit;
    private bool _SuppressDeathFX;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Sounds.PlayOneShot(SpawnSound);
    }

    private void OnEnable()
    {
        StartCoroutine(PlaySpawnAnimation());
    }

    private IEnumerator PlaySpawnAnimation()
    {
        Collider2D col = GetComponent<Collider2D>();
        col.enabled = false;
        float startTime = Time.time;
        float t = 0;
        if (SpawnAnimationDuration == 0) yield break;
        while (t < 1)
        {
            if (Time.time - startTime > 0.1f)
            {
                col.enabled = true;
            }
            t = (Time.time - startTime) / SpawnAnimationDuration;
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, SpawnAnimationCurve.Evaluate(t));
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

    public void OnApplicationQuit()
    {
        _Quit = true;
    }

    public void OnDestroy()
    {
        if (_Instances.Contains(this))
        {
            _Instances.Remove(this);
        }
        if (_Quit || Application.isPlaying == false) return;
        SpawnPop();
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        var menuController = collision.collider.GetComponentInChildren<MenuButtonEnemy>();
        if (menuController != null)
        {
            var pickup = Instantiate(EnemyPickupPrefab, menuController.transform.position, Quaternion.identity);
            pickup.GetComponent<EnemyPickup>().SetEnemy(menuController.gameObject);
            menuController.DoMenuAction();
            Destroy(gameObject);
            return;
        }
        
        var enemy = collision.collider.GetComponentInChildren<EnemyController>();
        if (enemy != null)
        {
            var projectileType = GetComponent<RockPaperScissorsComponent>().Value;
            var enemyType = enemy.GetComponent<RockPaperScissorsComponent>().Value;
            if (projectileType.Beats(enemyType)) // note: enemy wins on tie
            {
                BubbleCaptureEffect captureEffect = Instantiate(CaptureFXPrefab, transform.position, Quaternion.identity);
                captureEffect.PlayCaptureAnimation(enemy, transform.position);
                Destroy(enemy.gameObject);
                var pickup = Instantiate(EnemyPickupPrefab, enemy.transform.position, Quaternion.identity);
                pickup.GetComponent<EnemyPickup>().SetEnemy(enemy.gameObject);
                _SuppressDeathFX = true;
            }
            else if (_ActiveDenyEffect == null)
            {
                _ActiveDenyEffect = Instantiate(CaptureFXPrefab, transform.position, Quaternion.identity);
                _ActiveDenyEffect.PlayDenyAnimation(enemy, transform);
            }
            Destroy(gameObject);
        }
    }

    private void SpawnPop()
    {
        if (_SuppressDeathFX) return;
        if (PopVFXPrefab == null) return;
        Instantiate(PopVFXPrefab, transform.position, Quaternion.identity);
    }
}
