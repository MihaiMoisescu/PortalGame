using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject portalAPrefab;

    [SerializeField]
    private GameObject portalBPrefab;

    [SerializeField]
    private GameObject portalAOrbPrefab;

    [SerializeField]
    private GameObject portalBOrbPrefab;

    [SerializeField]
    private LayerMask portalSurfaceLayer;

    private GameObject currentPortalA;
    private GameObject currentPortalB;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        PortalOrb.OnOrbHitTarget += HandleOrbHitTarget;
    }

    void OnDestroy()
    {
        PortalOrb.OnOrbHitTarget -= HandleOrbHitTarget;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LaunchOrb(portalAOrbPrefab, portalAPrefab, ref currentPortalA, currentPortalB);
        }

        if (Input.GetMouseButtonDown(1))
        {
            LaunchOrb(portalBOrbPrefab, portalBPrefab, ref currentPortalB, currentPortalA);
        }
    }

    void LaunchOrb(GameObject orbPrefab, GameObject portalPrefab, ref GameObject currentPortal, GameObject otherPortal)
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, portalSurfaceLayer))
        {
            GameObject orb = Instantiate(orbPrefab, mainCamera.transform.position, Quaternion.identity);
            PortalOrb orbScript = orb.GetComponent<PortalOrb>();
            orbScript.Initialize(hitInfo.point, hitInfo.normal, portalPrefab, otherPortal, currentPortal);
        }
    }

    void HandleOrbHitTarget(Vector3 position, Vector3 normal, GameObject portalPrefab, GameObject otherPortal, GameObject currentPortal)
    {
        if (portalPrefab == portalAPrefab)
        {
            SpawnOrMovePortal(ref currentPortalA, position, normal, portalPrefab, otherPortal);
        }
        else if (portalPrefab == portalBPrefab)
        {
            SpawnOrMovePortal(ref currentPortalB, position, normal, portalPrefab, otherPortal);
        }
    }

    void SpawnOrMovePortal(ref GameObject currentPortal, Vector3 hitPosition, Vector3 hitNormal, GameObject portalPrefab, GameObject otherPortal)
    {
        BoxCollider currentCollider = currentPortal != null ? currentPortal.GetComponent<BoxCollider>() : null;
        if (currentCollider != null) currentCollider.enabled = false;

        if (IsOverlappingPortal(hitPosition, portalPrefab, hitNormal, otherPortal))
        {
            if (currentCollider != null) currentCollider.enabled = true;
            return;
        }

        Quaternion portalRotation = Quaternion.LookRotation(hitNormal);

        if (currentPortal == null)
        {
            currentPortal = Instantiate(portalPrefab, hitPosition, portalRotation);
        }
        else
        {
            currentPortal.transform.position = hitPosition;
            currentPortal.transform.rotation = portalRotation;
        }

        if (currentCollider != null) currentCollider.enabled = true;
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

        Vector3 boxSize = portalCollider.size * 0.98f; 
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
