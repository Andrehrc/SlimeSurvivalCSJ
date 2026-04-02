using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    private int damage;

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth health = other.GetComponent<EnemyHealth>();

        if (health != null)
        {
            health.TakeDamage(damage);
        }
    }

    public void GetDamage(int dmg)
    {
        damage = dmg;
    }
}
