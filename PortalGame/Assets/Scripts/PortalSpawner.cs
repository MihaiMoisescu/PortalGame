using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    public GameObject portalAPrefab;
    public GameObject portalBPrefab;
    public float spawnDistance = 10f;

    private GameObject currentPortalA;
    private GameObject currentPortalB;

    private Camera mainCamera;

    [SerializeField]
    public LayerMask portalSurfaceLayer;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnOrMovePortal(ref currentPortalA, portalAPrefab);
        }

        if (Input.GetMouseButtonDown(1))
        {
            SpawnOrMovePortal(ref currentPortalB, portalBPrefab);
        }
    }

    void SpawnOrMovePortal(ref GameObject currentPortal, GameObject portalPrefab)
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, portalSurfaceLayer))
        {
            Vector3 portalNormal = hitInfo.normal;
            Quaternion portalRotation = Quaternion.LookRotation(portalNormal);

            if (currentPortal == null)
            {
                currentPortal = Instantiate(portalPrefab, hitInfo.point, portalRotation);
            }
            else
            {
                currentPortal.transform.position = hitInfo.point;
                currentPortal.transform.rotation = portalRotation;
            }
        }
    }
}
