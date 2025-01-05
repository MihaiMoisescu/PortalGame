using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject portalAPrefab;

    [SerializeField]
    private GameObject portalBPrefab;

    [SerializeField]
    private LayerMask portalSurfaceLayer;

    private GameObject currentPortalA;
    private GameObject currentPortalB;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnOrMovePortal(ref currentPortalA, portalAPrefab, currentPortalB);
        }

        if (Input.GetMouseButtonDown(1))
        {
            SpawnOrMovePortal(ref currentPortalB, portalBPrefab, currentPortalA);
        }
    }

    void SpawnOrMovePortal(ref GameObject currentPortal, GameObject portalPrefab, GameObject otherPortal)
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, portalSurfaceLayer))
        {
            BoxCollider currentCollider = currentPortal != null ? currentPortal.GetComponent<BoxCollider>() : null;
            if (currentCollider != null) currentCollider.enabled = false;

            if (IsOverlappingPortal(hitInfo.point, portalPrefab, hitInfo.normal, otherPortal))
            {

                if (currentCollider != null) currentCollider.enabled = true;
                return;
            }

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

            if (currentCollider != null) currentCollider.enabled = true;
        }

    }

    bool IsOverlappingPortal(Vector3 position, GameObject portalPrefab, Vector3 surfaceNormal, GameObject otherPortal)
    {
        if (otherPortal == null) return false;

        BoxCollider portalCollider = portalPrefab.GetComponent<BoxCollider>();
        BoxCollider otherCollider = otherPortal.GetComponent<BoxCollider>();

        if (portalCollider == null || otherCollider == null)
        {
            return false;
        }

        Vector3 boxSize = portalCollider.size;
        Vector3 boxCenter = position + Quaternion.LookRotation(surfaceNormal) * portalCollider.center;

        Collider[] overlappingColliders = Physics.OverlapBox(boxCenter, boxSize / 2, Quaternion.LookRotation(surfaceNormal));
        foreach (Collider collider in overlappingColliders)
        {
            if (collider.gameObject == otherPortal)
            {
                return true;
            }
        }

        return false;
    }


}
