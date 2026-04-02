using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("UI")]

    public Image healthBar;

    public float maxHealth;
    public float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthBar.fillAmount = (float)(currentHealth / maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthBar.fillAmount = (float)(currentHealth / maxHealth);

    }

    public void AddMaxHealth(float amount)
    {
        maxHealth += amount;
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthBar.fillAmount = (float)(currentHealth / maxHealth);
    }

    void Die()
    {
        GameManager.instance.GameOver();
    }
}
