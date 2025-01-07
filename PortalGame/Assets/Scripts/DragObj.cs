using UnityEngine;

public class DragObj : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private float zCoordinate;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
