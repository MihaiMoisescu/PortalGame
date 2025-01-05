using UnityEngine;

public class PortalManager : MonoBehaviour
{
    private Transform APos;
    private Transform BPos;

    private bool isTeleporting = false;
    [SerializeField]
    private float teleportCooldown = 0.5f;

    private CapsuleCollider playerCollider;

    private void Start()
    {
        playerCollider = GetComponentInChildren<CapsuleCollider>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!col || isTeleporting) return;

        if (col.CompareTag("PortalOrb")) return;

        FindPortals();

        if (col.CompareTag("PortalA") && BPos != null)
        {
            StartCoroutine(TeleportPlayer(BPos));
        }
        else if (col.CompareTag("PortalB") && APos != null)
        {
            StartCoroutine(TeleportPlayer(APos));
        }
    }

    private System.Collections.IEnumerator TeleportPlayer(Transform targetPortal)
    {
        isTeleporting = true;

        CharacterController cc = GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        Vector3 targetPosition = targetPortal.position;

        if (Mathf.Abs(Vector3.Dot(targetPortal.up, Vector3.up)) < 0.5f)
        {
            targetPosition += targetPortal.up * 1.5f;
        }

        transform.position = targetPosition;

        Vector3 targetForward = targetPortal.forward;
        targetForward.y = 0;
        targetForward.Normalize();

        transform.rotation = Quaternion.LookRotation(targetForward, Vector3.up);

        if (cc != null) cc.enabled = true;

        yield return new WaitForSeconds(teleportCooldown);
        isTeleporting = false;
    }


    private void FindPortals()
    {
        GameObject portalA = GameObject.FindGameObjectWithTag("PortalA");
        GameObject portalB = GameObject.FindGameObjectWithTag("PortalB");

        if (portalA != null)
            APos = portalA.transform;

        if (portalB != null)
            BPos = portalB.transform;
    }
}
