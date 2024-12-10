using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    public GameObject portalAPrefab;
    public GameObject portalBPrefab; 
    public float spawnDistance = 10f; 

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
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
        {
            Vector3 directionToCamera = (mainCamera.transform.position - hitInfo.point).normalized;

            Quaternion portalRotation = Quaternion.LookRotation(directionToCamera);

            if (currentPortal != null)
            {
                currentPortal.transform.position = hitInfo.point;
                currentPortal.transform.rotation = portalRotation;
            }
            else
            {
                currentPortal = Instantiate(portalPrefab, hitInfo.point, portalRotation);
            }
        }
        else
        {
            Vector3 spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * spawnDistance;

            Vector3 directionToCamera = (mainCamera.transform.position - spawnPosition).normalized;

            Quaternion portalRotation = Quaternion.LookRotation(directionToCamera);

            if (currentPortal != null)
            {
                currentPortal.transform.position = spawnPosition;
                currentPortal.transform.rotation = portalRotation;
            }
            else
            {
                currentPortal = Instantiate(portalPrefab, spawnPosition, portalRotation);
            }
        }
    }

}
