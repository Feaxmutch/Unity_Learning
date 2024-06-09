using UnityEngine;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private string _volumeName;

    private float _savedVolume;

    public void SetVolume(float value)
    {
        float volume = value > 0 ? Mathf.Log10(value) * 20 : -80;
        _mixer.SetFloat(_volumeName, volume);
    }

    public void ToggleVolume(bool state)
    {
        if (state)
        {
            _mixer.SetFloat(_volumeName, _savedVolume);
        }
        else
        {
            _mixer.GetFloat(_volumeName, out _savedVolume);
            _mixer.SetFloat(_volumeName, -80);
        }
    }
}
