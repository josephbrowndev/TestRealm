using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] int maxHealth = 100;
    public bool IsInvulnerable { get; set; }
    public int CurrentHealth { get; private set;  }
    
    public event System.Action<int> OnHealthChanged;
    public event System.Action OnDied;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (IsInvulnerable) return;
        if (CurrentHealth <= 0) return;

        CurrentHealth = Mathf.Max(CurrentHealth - amount, 0);
        OnHealthChanged?.Invoke(CurrentHealth);
        if (CurrentHealth == 0)
        {
            OnDied?.Invoke();
        }

    }

    
}
