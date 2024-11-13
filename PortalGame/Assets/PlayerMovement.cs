using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputAction MoveAction;
    [SerializeField] private float _movementSpeed = 3.0f;
    private Transform _transform;
    void Start()
    {
        _transform = gameObject.GetComponent<Transform>();
        MoveAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = MoveAction.ReadValue<Vector3>();
        Debug.Log(move);
        Vector3 position = (Vector3)_transform.position + move * _movementSpeed * Time.deltaTime;
        _transform.position = position;
    }
}
