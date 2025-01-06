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
        if(this.tag=="HugeDoor")
            _animator.Play("DoorAnimation");
        else if(this.tag=="SmallDoor")
        {
            _animator.Play("SmallDoorAnimation");
        }
    }
    public void CloseDoor()
    {
        if (_audioClip != null)
        {
            _audioSource.PlayOneShot(_audioClip);
        }
        if (this.tag == "SmallDoor")
        {
            _animator.Play("SmallDoorAnimationClose");
        }
    }
    void Update()
    {
        
    }
}
