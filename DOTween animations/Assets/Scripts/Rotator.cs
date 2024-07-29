using DG.Tweening;
using UnityEngine;

public class Rotator : DOTweenAnimator
{
    [SerializeField] private Vector3 _axisRotation;

    private void Start()
    {
        transform.DORotate(transform.rotation.eulerAngles + _axisRotation.normalized, Duration)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);
    }
}
