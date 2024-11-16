using UnityEngine;

public class PortalController : MonoBehaviour
{
    public GameObject m_PortalOut;

    private void OnCollisionEnter(Collision collision)
    {
        if(m_PortalOut != null)
        {
            collision.gameObject.transform.position= m_PortalOut.transform.position + collision.gameObject.transform.forward*8;
        }
    }

    public void setOutPortal(GameObject portal)
    {
        m_PortalOut = portal;
    }

}
