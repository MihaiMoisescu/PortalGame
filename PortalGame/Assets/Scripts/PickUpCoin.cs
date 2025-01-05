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

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            coinCounter++;
            Destroy(collision.gameObject);
        }
    }
}
