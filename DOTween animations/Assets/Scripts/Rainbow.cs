using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Rainbow : DOTweenAnimator
{
    private MeshRenderer _meshRenderer;
    private Tweener _tweener;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _tweener = _meshRenderer.material.DOColor(Random.ColorHSV(), Duration).SetAutoKill(false);
    }

    private void OnEnable()
    {
        _tweener.onPause += RestartTweener;
    }

    private void OnDisable()
    {
        _tweener.onPause -= RestartTweener;
    }

    private void RestartTweener()
    {
        _tweener.ChangeEndValue(Random.ColorHSV(), true).Restart();
    }
}
