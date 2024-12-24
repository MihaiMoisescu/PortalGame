using UnityEngine;
using UnityEngine.SceneManagement; 

public class LaserScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
        
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
