using UnityEngine;

public class PickUpCoin : MonoBehaviour
{
    private int coinCounter = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public int CoinCounter { get => coinCounter; set => coinCounter = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coinCounter++;
            Destroy(other.gameObject);
        }
    }
}
