using UnityEngine;

public class PlayerDoorInteraction : MonoBehaviour
{
    [SerializeField] private GameObject _FirstDoor;
    [SerializeField] private GameObject _SecondDoor;
    [SerializeField] private GameObject _ThirdDoor;
    [SerializeField] private PickUpCoin _PickUpCoin;
    private Door _firstDoor;
    private Door _secondDoor;
    private Door _thirdDoor;
    private bool _isOnTrigger;
    private bool _hasOpened;

    private void Start()
    {
        if (_FirstDoor != null)
        {
            _firstDoor = _FirstDoor.GetComponent<Door>();
            _secondDoor = _SecondDoor.GetComponent<Door>();
            _thirdDoor = _ThirdDoor.GetComponent<Door>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OpenFirstDoor"))
        {
            _isOnTrigger = true;
        }
        if (other.CompareTag("Finish")&&_PickUpCoin.CoinCounter==2)
        {
            _secondDoor.OpenDoor();
        }
        if (other.CompareTag("Finish") && _PickUpCoin.CoinCounter == 12)
        {
            _thirdDoor.OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("OpenFirstDoor"))
        {
            _isOnTrigger = false;
        }
    }

    private void Update()
    {

        if (_isOnTrigger && Input.GetKeyDown(KeyCode.E) && !_hasOpened)
        {
            _firstDoor.OpenDoor();
            _hasOpened = true; 
        }
    }
}
