using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class MeleeAttack : MonoBehaviour
{
    [Header("Targeting")]
    [SerializeField] private LayerMask hitMask;
    [SerializeField] private float detectionRadius = 10f;

    [Header("Melee")]
    [SerializeField] private bool useMelee = true;
    [SerializeField] private float meleeRange = 2f;
    [SerializeField] private float meleeOffset = 1f;
    [SerializeField] private float meleeRadius = 1f;
    [SerializeField] private int meleeDamage = 25;
    [SerializeField] private float meleeInterval = 0.4f;

    [Header("Range")]
    [SerializeField] private bool useRange = true;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float projectileSpeed = 15f;
    [SerializeField] private int rangeDamage = 10;
    [SerializeField] private float rangeInterval = 1f;
    [SerializeField] private float spawnOffset = 1f;

    

    private float nextMeleeTime;
    private float nextRangeTime;




    private void Update()
    {

        // Is attack ready?
        bool meleeReady = useMelee && Time.time >= nextMeleeTime;
        bool rangeReady = useRange && Time.time >= nextRangeTime;
        if (!meleeReady && !rangeReady) return;

        // Is there an enemy??
        Transform target = FindClosestEnemy();
        if (target == null) return;

        //melee if target is close enough
        float sqrDistance = (target.position - transform.position).sqrMagnitude;
        if (meleeReady && sqrDistance <= meleeRange * meleeRange)
        {
            MeleAttack(target);
            nextMeleeTime = Time.time + meleeInterval;
        }

        // range fire in range
        if (rangeReady)
        {
            RangeAttack(target);
            nextRangeTime = Time.time + rangeInterval;
        }
    }
    
    private Transform FindClosestEnemy()
    {
        Collider[] candidates = Physics.OverlapSphere(transform.position, detectionRadius, hitMask);
        Transform closest = null;
        float closestSqrDistance = Mathf.Infinity;
        
        foreach (Collider candidate in candidates)
        {
            float sqrDistance = (candidate.transform.position - transform.position).sqrMagnitude;
            if (sqrDistance < closestSqrDistance)
            {
                closestSqrDistance = sqrDistance;
                closest = candidate.transform;
            }
        }
        return closest;

    }
   private void MeleAttack (Transform target)
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0f;
        direction.Normalize();

        Vector3 strikePoint = transform.position + direction * meleeOffset;
        Collider[] hits = Physics.OverlapSphere(strikePoint, meleeRadius, hitMask);
            foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(meleeDamage);
            }
        }
    }
    private void RangeAttack(Transform target)
    {
        if (projectilePrefab == null) return;
        Vector3 direction = (target.position - transform.position).normalized;
        Vector3 spawnPosition = transform.position + direction * spawnOffset;

        Projectile shot = Instantiate(projectilePrefab, spawnPosition, Quaternion.LookRotation(direction));
        shot.Launch(direction * projectileSpeed, rangeDamage, hitMask);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeRange);
    }

}
