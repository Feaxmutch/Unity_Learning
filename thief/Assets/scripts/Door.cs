using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider))]
public class Door : MonoBehaviour
{
    private const string IsOpen = "isOpen";

    private Animator _animator;
    private BoxCollider _openTriger;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _openTriger = GetComponent<BoxCollider>();
    }


    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Thief thief))
        {
            _animator.SetBool(IsOpen, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Thief thief))
        {
            _animator.SetBool(IsOpen, false);
        }
    }
}
