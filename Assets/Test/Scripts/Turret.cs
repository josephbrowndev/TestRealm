using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Rigidbody projectilePrefab;
    [SerializeField] private float fireInterval = 2f;
    [SerializeField] private float projectileSpeed = 10f;

    private void Start()
    {
        StartCoroutine(FireLoop());
    }

    private IEnumerator FireLoop()
    {
        while(true)
        {
            yield return new WaitForSeconds(fireInterval);
            Fire();
        }
    }
    private void Fire()
    {
        Vector3 toTarget = (target.position - transform.position).normalized;
        Rigidbody shot = Instantiate(projectilePrefab, transform.position + toTarget, Quaternion.identity);
        shot.linearVelocity = toTarget * projectileSpeed;
    }
}
