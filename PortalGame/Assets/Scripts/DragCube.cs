using UnityEngine;

public class DragObject : MonoBehaviour
{
    [SerializeField] private GameObject Door;
    private bool isDragging = false;
    private Vector3 offset;
    private float zCoordinate;
    private Rigidbody rb;
    private Door _door;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _door=Door.GetComponent<Door>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="OpenThirdDoor")
        {
            _door.OpenDoor();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag=="OpenThirdDoor")
        {
            _door.CloseDoor();
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                zCoordinate = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
                offset = gameObject.transform.position - GetMouseWorldPos();
                isDragging = true;

                rb.useGravity = false;
                rb.isKinematic = true; 
            }
        }

        if (Input.GetMouseButtonUp(2))
        {
            isDragging = false;
            rb.useGravity = true;
            rb.isKinematic = false; 
        }

        if (isDragging)
        {
            transform.position = GetMouseWorldPos() + offset;
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoordinate;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
