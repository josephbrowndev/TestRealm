using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    public int Total { get; private set; }
    public event System.Action<int> OnTotalChanged;

    public void Add(int amount)
    {
        Total += amount;
        OnTotalChanged?.Invoke(Total);
    }
}
