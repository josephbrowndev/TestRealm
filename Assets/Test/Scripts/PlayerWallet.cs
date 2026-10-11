using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    public int total { get; private set; }
    public event System.Action<int> OnTotalChanged;

    public void Add(int amount)
    {
        total += amount;
        OnTotalChanged?.Invoke(total);
    }

    


}
