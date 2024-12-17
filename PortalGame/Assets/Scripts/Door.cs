using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

    }
    public void OpenDoor()
    {
        if (_audioClip != null)
        {
            _audioSource.PlayOneShot(_audioClip);
        }
            _animator.Play("DoorAnimation");
    }
    void Update()
    {
        
    }
}
