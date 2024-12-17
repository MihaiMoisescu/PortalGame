using UnityEngine;

public class PlayerDoorInteraction : MonoBehaviour
{
    [SerializeField] private GameObject _FirstDoor;
    [SerializeField] private GameObject _SecondDoor;
    [SerializeField] private GameObject _ThirdDoor;

    private Door _door;
    private bool _isOnTrigger;
    private bool _hasOpened;  // Variabilă de control pentru o singură deschidere

    private void Start()
    {
        if (_FirstDoor != null)
        {
            _door = _FirstDoor.GetComponent<Door>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OpenFirstDoor"))
        {
            _isOnTrigger = true;
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
            _door.OpenDoor();
            _hasOpened = true; 
        }
    }
}
