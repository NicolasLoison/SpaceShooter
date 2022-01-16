using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BossHealth : MonoBehaviour
{
    public bool isDamaged = false;
    public float damageFlashDelay = 0.2f;
    public float damageFlashDuration = 1f;
    private SpriteRenderer[] _spriteRenderers;  // Liste des sprites du vaisseau et ses enfants
    public GameObject[] impactPoints;
    public float hp;
    public float maxHp;
    public static BossHealth Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de BossHealth dans la scène !");
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        hp = maxHp;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
            return;
        }
        isDamaged = true;
        StartCoroutine(DamageFlash());
        StartCoroutine(HandleDamageFlashDelay());
    }

    public void Die()
    {
        ExplodeShip();
        BonusSpawner.Instance.SpawnHealthBonus(1, gameObject);
        SoundManager.Instance.RocketExplosionSound();
        SoundManager.Instance.PlayNextSong();
        GameManager.Instance.AddScorePlayer(50);
        GameManager.Instance.ResetGameScenario();
        Destroy(gameObject);
    }

    private void ExplodeShip()
    {
        foreach (var impact in impactPoints)
        {
            Instantiate(Resources.Load("Impact03"), impact.transform.position, Quaternion.identity);
        }
        foreach (var spriteRenderer in _spriteRenderers) spriteRenderer.enabled = false;
    }

    public IEnumerator DamageFlash()
    {
        while (isDamaged)
        {
            // On boucle sur les sprites du vaisseau et ses enfants pour les faire clignoter
            foreach (var spriteRenderer in _spriteRenderers) spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(damageFlashDelay);
            foreach (var spriteRenderer in _spriteRenderers) spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(damageFlashDelay);
        }
    }
    
    public IEnumerator HandleDamageFlashDelay()
    {
        yield return new WaitForSeconds(damageFlashDuration);
        isDamaged = false;
    }
}
