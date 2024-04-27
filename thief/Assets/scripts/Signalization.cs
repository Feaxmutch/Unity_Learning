using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Signalization : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _clip;
        _audioSource.volume = 0;
        _audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Thief thief))
        {
            StopAllCoroutines();
            StartCoroutine(AlarmVolumeUp());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Thief thief))
        {
            StopAllCoroutines();
            StartCoroutine(AlarmVolumeDown());
        }
    }

    private IEnumerator AlarmVolumeDown()
    {
        while (enabled)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, 0, Time.deltaTime * 0.01f);
            yield return null;
        }
    }

    private IEnumerator AlarmVolumeUp()
    {
        while (enabled)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, 0.3f, Time.deltaTime * 0.01f);
            yield return null;
        }
    }
}
