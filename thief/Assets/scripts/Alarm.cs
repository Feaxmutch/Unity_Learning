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
        if (_interpolatingVolume == null)
        {
            _interpolatingVolume = StartCoroutine(InterpolatingVolume(interpolatingSpeed));
        }
    }

    public void StopInterpolating()
    {
        if (_interpolatingVolume != null)
        {
            StopCoroutine(_interpolatingVolume);
            _interpolatingVolume = null;
        }
    }

    private IEnumerator InterpolatingVolume(float interpolatingSpeed)
    {
        float targetVolume = interpolatingSpeed > 0 ? _maxVolume : _minVolume;
        interpolatingSpeed = Mathf.Abs(interpolatingSpeed);

        while (enabled)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, Time.deltaTime * interpolatingSpeed);

            if (_audioSource.volume > _minVolume && _audioSource.isPlaying == false)
            {
                _audioSource.Play();
            }
            else if (_audioSource.volume == _minVolume && _audioSource.isPlaying)
            {
                _audioSource.Stop();
            }

            yield return null;
        }
    }
}
