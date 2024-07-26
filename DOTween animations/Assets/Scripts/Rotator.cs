using DG.Tweening;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 _axisRotation;
    [SerializeField] private float _duration;

    private void Start()
    {
        transform.DORotate(transform.rotation.eulerAngles + _axisRotation.normalized, _duration)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);
    }
}
