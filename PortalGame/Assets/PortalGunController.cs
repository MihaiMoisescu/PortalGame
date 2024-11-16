using UnityEngine;
using UnityEngine.Rendering;

public class PortalGunController : MonoBehaviour
{
    public float shootRate;
    private float m_shootrateTimeStamp;

    RaycastHit hit;
    float range = 1000f;

    public GameObject m_PortalRed;
    public GameObject m_PortalBlue;

    private GameObject m_CurrentPortalRed;
    private GameObject m_CurrentPortalBlue;

    private void Update()
    {
        if(Time.time > m_shootrateTimeStamp)
        {
            if(Input.GetMouseButton(0))
            {
                shotPortalRed(m_PortalRed);
                m_shootrateTimeStamp = Time.time + shootRate;

            }

            if(Input.GetMouseButton(1))
            {
                shotPortalBlue(m_PortalBlue);
                m_shootrateTimeStamp= Time.time + shootRate;
            }
        }
    }

     void shotPortalRed(GameObject portal)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit,range))
        {
            Destroy(m_CurrentPortalRed);
            m_CurrentPortalRed = Instantiate(portal, hit.point, transform.rotation);
            setOutPortals();
        }
    }

    void shotPortalBlue(GameObject portal)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, range))
        {
            Destroy(m_CurrentPortalBlue);
            m_CurrentPortalBlue = Instantiate(portal, hit.point, transform.rotation);
            setOutPortals();
        }
    }

    void setOutPortals()
    {
        m_CurrentPortalRed.GetComponent<PortalController>().setOutPortal(m_CurrentPortalBlue);
        m_CurrentPortalBlue.GetComponent<PortalController>().setOutPortal(m_CurrentPortalRed);
    }
}
