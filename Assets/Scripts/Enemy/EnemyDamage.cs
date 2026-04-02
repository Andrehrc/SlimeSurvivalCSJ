using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damage;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
