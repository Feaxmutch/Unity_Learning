using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    [SerializeField] private float _maxVolume;
    [SerializeField] private float _minVolume;
    
    private AudioSource _audioSource;
    private Coroutine _interpolatingVolume;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _clip;
        _audioSource.volume = _minVolume;
    }

    public void InterpolateVolume(float interpolatingSpeed)
    {
        if (_interpolatingVolume != null)
        {
            StopCoroutine(_interpolatingVolume);
        }

        _interpolatingVolume = StartCoroutine(InterpolatingVolume(interpolatingSpeed));
    }

    private IEnumerator InterpolatingVolume(float interpolatingSpeed)
    {
        float targetVolume = interpolatingSpeed > 0 ? _maxVolume : _minVolume;
        interpolatingSpeed = Mathf.Abs(interpolatingSpeed);
        _audioSource.Play();

        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, Time.deltaTime * interpolatingSpeed);
            yield return null;
        }

        if (_audioSource.volume == _minVolume)
        {
            _audioSource.Stop();
        }
    }
}
