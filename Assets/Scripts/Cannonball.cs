using UnityEngine;

public class Cannonball : MonoBehaviour
{
    public float Damage { get; private set; } = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
