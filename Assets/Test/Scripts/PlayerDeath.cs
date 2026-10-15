using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private PlayerMover movement;
    [SerializeField] private PlayerDash dash;
 
    private void OnEnable() => health.OnDied += HandleDied;
    private void OnDisable() => health.OnDied -= HandleDied;

    private void HandleDied()
    {
        Debug.Log("Player Died");
        movement.enabled = false;
        dash.enabled = false;
        
    }
}
