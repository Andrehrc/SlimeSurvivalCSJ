using UnityEngine;

public class AxeProjectile : MonoBehaviour
{
    public float horizontalForce;
    public float verticalForce;

    Rigidbody2D rig;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();

        float direction = Random.value < 0.5f ? -1f : 1f;

        Vector2 force = new Vector2(horizontalForce * direction, verticalForce);

        rig.AddForce(force, ForceMode2D.Impulse);
    }
}
