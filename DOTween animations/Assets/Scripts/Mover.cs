using DG.Tweening;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _duration;

    private void Start()
    {
        transform.DOMove(transform.position + _offset, _duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
}
