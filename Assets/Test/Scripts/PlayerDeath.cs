using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private PlayerMover movement;

    private void OnEnable() => health.OnDied += HandleDied;
    private void OnDisable() => health.OnDied -= HandleDied;

    private void HandleDied()
    {
        Debug.Log("Player Died");
        movement.enabled = false;
    }
}
