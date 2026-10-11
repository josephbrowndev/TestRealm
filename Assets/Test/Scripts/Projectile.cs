using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage = 100;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable target))
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
