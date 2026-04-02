using Unity.Hierarchy;
using UnityEngine;

public class XPOrb : MonoBehaviour
{
    public int xpValue = 1;
    public float speed;
    public float detecRadius;

    private Transform target;

    private void Start()
    {
        target = PlayerController.Instance.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerXP>().AddXp(xpValue);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (target != null)
        {
            float distance = Vector2.Distance(transform.position, target.position);

            if (distance <= detecRadius)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detecRadius);
    }
}


