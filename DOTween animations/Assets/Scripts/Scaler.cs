using DG.Tweening;
using UnityEngine;

public class Scaler : DOTweenAnimator
{
    [SerializeField] private Vector3 _scaleOffset;

    private void Start()
    {
        transform.DOScale(transform.localScale + _scaleOffset, Duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
}
