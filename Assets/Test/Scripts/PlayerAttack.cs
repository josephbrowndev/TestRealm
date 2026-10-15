using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private InputActionReference attackAction;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 1f;
    [SerializeField] private int damage = 25;
    [SerializeField] private float cooldown = 0.4f;
    [SerializeField] private LayerMask hitMask;

    private bool isAttacking;
    private float nextAttackTime;


    private void OnEnable() => attackAction.action.Enable();
    private void OnDisable() => attackAction.action.Disable();
    private void Update()
    {
        if (isAttacking || Time.time < nextAttackTime) return;

        if (attackAction.action.WasPressedThisFrame())
        {
            Attack();
        }
        }

    private void Attack()
    {
        Debug.Log("I am attacking");
        if (attackPoint == null) return;
        Collider[] hitColliders = Physics.OverlapSphere(attackPoint.position, attackRadius, hitMask);
        
        foreach (Collider col in hitColliders)
        {
            Debug.Log(col.name);
            if (col.TryGetComponent(out IDamageable damageable))
            {

                damageable.TakeDamage(damage);
            }
        }

        isAttacking = false;
        nextAttackTime = Time.time + cooldown;

    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }


}
