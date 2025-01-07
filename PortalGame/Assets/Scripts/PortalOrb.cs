using UnityEngine;
using System;

public class PortalOrb : MonoBehaviour
{
    public static event Action<Vector3, Vector3, GameObject, GameObject, GameObject> OnOrbHitTarget;

    private Vector3 targetPosition;
    private Vector3 targetNormal;
    private GameObject portalPrefab;
    private GameObject otherPortal;
    private GameObject currentPortal;

    private float speed = 45f;

    public void Initialize(Vector3 targetPosition, Vector3 targetNormal, GameObject portalPrefab, GameObject otherPortal, GameObject currentPortal)
    {
        this.targetPosition = targetPosition;
        this.targetNormal = targetNormal;
        this.portalPrefab = portalPrefab;
        this.otherPortal = otherPortal;
        this.currentPortal = currentPortal;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            EmitSignal();
        }
    }

    private void EmitSignal()
    {
        OnOrbHitTarget?.Invoke(targetPosition, targetNormal, portalPrefab, otherPortal, currentPortal);
        Destroy(gameObject); 
    }
}
