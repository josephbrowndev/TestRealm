using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] int maxHealth = 100;
    public int currentHealth { get; private set;  }
    public PlayerMover movement;

    public event System.Action<int> OnHealthChanged;
    public event System.Action OnDied;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0) return;

        currentHealth = Mathf.Max(currentHealth - amount, 0);
        OnHealthChanged?.Invoke(currentHealth);
        if (currentHealth == 0)
        {
            OnDied?.Invoke();
        }

    }

    
}
