
using UnityEngine;
using UnityEngine.SceneManagement; 
public class PlayerDeath : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    [SerializeField] private  AudioClip deathSound;
    private AudioSource audioSource; 

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _playerMovement = GetComponent<PlayerMovement>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    public void Die() 
    { 
        if (deathSound != null)
        {
            audioSource.PlayOneShot(deathSound); 
        }
        Invoke("LoadGameOverScene", 3);
    }
    void LoadGameOverScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lava")||other.gameObject.CompareTag("Laser"))
        {
           _playerMovement.enabled = false;
            Die(); 
        }
    }
}


