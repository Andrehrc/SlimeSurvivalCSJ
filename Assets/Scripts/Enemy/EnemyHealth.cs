using System.Collections;
using UnityEngine;

/// <summary>
/// Responsável por tirar vida e destruir inimigos
/// </summary>

public class EnemyHealth : MonoBehaviour
{
    [Header("Renderer")]
    public SpriteRenderer spriteRenderer;
    public Material hitMaterial;
    public float flashTime;
    public GameObject canvaHitPrefab;

    private Material currentMaterial;

    private Rigidbody2D rig;

    [Header("Status")]
    public GameObject dieEffect;
    public Color dieEffectColor;
    public int maxLife;

    int currentLife;

    Coroutine flashCoroutine;

    private void Awake()
    {
        currentLife = maxLife;
        currentMaterial = spriteRenderer.sharedMaterial;
        rig = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
        currentLife -= damage;

        ApplyKnockback(PlayerController.Instance.transform.position, 8f, 0.1f);

        GameObject hit = Instantiate(canvaHitPrefab, transform.position, Quaternion.identity);
        hit.transform.SetParent(transform);
        hit.GetComponent<HitCanva>().ChangeValue(damage);
        Destroy(hit, 2);

        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }

        flashCoroutine = StartCoroutine(FlashCorroutine());

        if (currentLife <= 0)
        {
            Die();
        }
    }

    IEnumerator FlashCorroutine()
    {
        spriteRenderer.sharedMaterial = hitMaterial;
        yield return new WaitForSeconds(flashTime);
        spriteRenderer.sharedMaterial = currentMaterial;

        flashCoroutine = null;
    }


    void Die()
    {
        GameManager.instance.EnemyCount();
        Instantiate(GameManager.instance.xpOrb, transform.position, GameManager.instance.xpOrb.transform.rotation);
        GameObject effect = Instantiate(dieEffect, transform.position, Quaternion.identity);

        var ps = effect.GetComponentInChildren<ParticleSystem>();
        if (ps != null)
        {
            var main = ps.main;
            main.startColor = new Color(dieEffectColor.r, dieEffectColor.g, dieEffectColor.b, 1f);
        }

        Destroy(effect, 2);
        Destroy(gameObject);
    }

    #region KnockBack

    public bool isKnockedBack;
    Coroutine knockbackRoutine;

    public void ApplyKnockback(Vector2 direction, float force, float duration)
    {
        Vector2 dir = ((Vector2)transform.position - direction).normalized;

        if (knockbackRoutine != null)
        {
            StopCoroutine(knockbackRoutine);
        }

        knockbackRoutine = StartCoroutine(KnockbackRoutine(dir, force, duration));
    }

    IEnumerator KnockbackRoutine(Vector2 direction, float force, float duration)
    {
        isKnockedBack = true;
        rig.linearVelocity = direction * force;
        yield return new WaitForSeconds(duration);

        rig.linearVelocity = Vector2.zero;
        isKnockedBack = false;

        knockbackRoutine = null;
    }

    #endregion
}
