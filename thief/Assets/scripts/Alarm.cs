using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    [SerializeField] private float _maxVolume;
    [SerializeField] private float _minVolume;
    
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _clip;
        _audioSource.volume = _minVolume;
        _audioSource.Play();
    }

    public void InterpolateVolume(float interpolatingSpeed)
    {
        StartCoroutine(InterpolatingVolume(interpolatingSpeed));
    }

    public void StopInterpolating()
    {
        StopAllCoroutines();
    }

    private IEnumerator InterpolatingVolume(float interpolatingSpeed)
    {
        float targetVolume = interpolatingSpeed > 0 ? _maxVolume : _minVolume;

        while (enabled)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, Time.deltaTime * interpolatingSpeed);
            yield return null;
        }
    }
}
