using UnityEngine;

public class DestroyOnDeath : MonoBehaviour
{
    [SerializeField] private Health health;

    private void OnEnable() => health.OnDied += HandleDied;
    private void OnDisable() => health.OnDied -= HandleDied;

    private void HandleDied()
    {
        Destroy (gameObject);

    }
}
