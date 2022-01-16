using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShipHealth : MonoBehaviour
{
    public bool isInvincible = false;
    public float invincibilityFlashDelay = 0.2f;
    public float invincibilityTimeAfterHit = 2f;

    public Slider healthBar;
    public Color low, high;
    public float hpShip;
    public float maxHp;

    private SpriteRenderer[] _spriteRenderers;  // Liste des sprites du vaisseau et ses enfants
    public static ShipHealth Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de ShipHealth dans la scène !");
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        _spriteRenderers = moveShip.Instance.GetComponentsInChildren<SpriteRenderer>();
        hpShip = maxHp;
        healthBar.maxValue = maxHp;
        healthBar.value = hpShip;
        healthBar.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, healthBar.normalizedValue);
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;
        hpShip -= damage;
        if (hpShip <= 0)
        {
            Die();
            healthBar.value = 0;
            return;
        }

        healthBar.value = hpShip;
        healthBar.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, healthBar.normalizedValue);
        isInvincible = true;
        StartCoroutine(InvincibilityFlash());
        StartCoroutine(HandleInvincibilityDelay());
    }

    public void Die()
    {
        GameObject impact =
            Instantiate(Resources.Load("Impact01"), transform.position, Quaternion.identity) as GameObject;
        // On boucle sur le vaisseau et ses enfants pour les désactiver 
        DesactivateShipCommand();
        foreach (var spriteRenderer in GetSpriteChildren()) spriteRenderer.enabled = false;
        GameOverManager.Instance.GameOver();
    }

    public void DesactivateShipCommand()
    {
        moveShip.Instance.enabled = false;
        moveShip.Instance.rb.velocity = Vector3.zero;
        moveShip.Instance.polygonCollider2D.enabled = false;
        moveShip.Instance.GetComponentInChildren<BoxCollider2D>().enabled = false;
        foreach (var boxCollider in GetBoxColliderChildren()) boxCollider.enabled = false;
    }

    public IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            // On boucle sur les sprites du vaisseau et ses enfants pour les faire clignoter
            foreach (var spriteRenderer in GetSpriteChildren()) spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
            foreach (var spriteRenderer in GetSpriteChildren()) spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
        }
    }
    
    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(invincibilityTimeAfterHit);
        isInvincible = false;
    }

    public SpriteRenderer[] GetSpriteChildren()
    {
        return GetComponentsInChildren<SpriteRenderer>();
    }
    public BoxCollider2D[] GetBoxColliderChildren()
    {
        return GetComponentsInChildren<BoxCollider2D>();
    }

    public void BonusHp(float hpBonus)
    {
        if (hpShip + hpBonus >= maxHp)
        {
            hpShip = maxHp;
        }
        else
        {
            hpShip += hpBonus;
        }
        healthBar.value = hpShip;
        healthBar.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, healthBar.normalizedValue);
    }
}
