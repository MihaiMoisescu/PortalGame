using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickUpCoin : MonoBehaviour
{
    private int coinCounter = 0;
    [SerializeField] private TextMeshProUGUI coinText;
    public int CoinCounter { get => coinCounter; }
    private void Start()
    {
        UpdateCoinUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coinCounter++;
            UpdateCoinUI();
            Destroy(other.gameObject); 
        }
    }

    private void UpdateCoinUI()
    {
        coinText.text = "Coins: " + coinCounter.ToString();
    }
}
