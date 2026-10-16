using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage = 100;
    [SerializeField] private LayerMask hitMask = -0;
    [SerializeField] private float lifetime = 5f;
    
    public void Launch(Vector3 velocitiy, int newDamage, LayerMask newHitMask)
    {
        damage = newDamage;
        hitMask = newHitMask;
        GetComponent<Rigidbody>().linearVelocity = velocitiy;
    }

    private void Start() => Destroy(gameObject, lifetime);
  
    private void OnTriggerEnter(Collider other)
    {

        bool layerIsInMask = (hitMask.value & (1 << other.gameObject.layer)) != 0;
        if (!layerIsInMask) return;
        if (other.TryGetComponent(out IDamageable target))
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
