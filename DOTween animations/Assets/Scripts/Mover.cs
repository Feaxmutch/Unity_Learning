using DG.Tweening;
using UnityEngine;

public class Mover : DOTweenAnimator
{
    [SerializeField] private Vector3 _offset;

    private void Start()
    {
        transform.DOMove(transform.position + _offset, Duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
}
