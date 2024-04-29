using UnityEngine;

[RequireComponent(typeof(Alarm))]
public class Signalization : MonoBehaviour
{
    [SerializeField] private float _volumeSpeed;
    
    private Alarm _alarm;

    private void Start()
    {
        _alarm = GetComponent<Alarm>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Thief thief))
        {
            _alarm.InterpolateVolume(_volumeSpeed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Thief thief))
        {
            _alarm.InterpolateVolume(_volumeSpeed * -1);
        }
    }

}
