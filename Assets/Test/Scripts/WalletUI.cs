using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class WalletUI : MonoBehaviour
{
    [SerializeField] private PlayerWallet wallet;
    [SerializeField] private TMP_Text label;

    private void OnEnable()
    {
        wallet.OnTotalChanged += HandleTotalChanged;
    }

    private void OnDisable()
    {
        wallet.OnTotalChanged -= HandleTotalChanged;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        HandleTotalChanged(wallet.total);
    }

    private void HandleTotalChanged(int newTotal)
    {
        label.text = $"Coins: {newTotal}";
    }

}
