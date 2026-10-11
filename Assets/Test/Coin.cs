using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField] private int score;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hey something is here");
        if (other.TryGetComponent(out PlayerWallet wallet))
        {
            
            score++;
            Debug.Log(score);
            Destroy(gameObject);
        }
    }
}
