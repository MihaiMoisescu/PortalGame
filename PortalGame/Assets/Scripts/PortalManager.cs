using UnityEngine;

public class PortalManager : MonoBehaviour
{
    private Transform APos;
    private Transform BPos;

    private bool isTeleporting = false;
    public float teleportCooldown = 0.5f;

    private void OnTriggerEnter(Collider col)
    {
        if (isTeleporting) return;

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

        cc.enabled = false;

        transform.position = targetPortal.position;
        transform.rotation = Quaternion.Euler(
            transform.rotation.eulerAngles.x,
            targetPortal.rotation.eulerAngles.y,
            transform.rotation.eulerAngles.z
        );

        cc.enabled = true;

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
