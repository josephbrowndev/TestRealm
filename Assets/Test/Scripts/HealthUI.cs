using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private TMP_Text label;

    private void OnEnable() => health.OnHealthChanged += HandleHealthChanged;
    private void OnDisable() => health.OnHealthChanged -= HandleHealthChanged;

    private void Start() => HandleHealthChanged(health.currentHealth);

    private void HandleHealthChanged(int currentHealth)
    {
        label.text = $"Health: {currentHealth}";
    }

}
