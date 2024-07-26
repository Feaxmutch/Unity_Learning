using DG.Tweening;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField] private Vector3 _scaleOffset;
    [SerializeField] private float _duration;

    private void Start()
    {
        transform.DOScale(transform.localScale + _scaleOffset, _duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
}
