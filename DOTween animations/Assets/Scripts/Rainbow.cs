using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Rainbow : MonoBehaviour
{
    [SerializeField] private float _duration;

    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        StartCoroutine(RepitedColoring());
    }

    private IEnumerator RepitedColoring()
    {
        while (enabled)
        {
            Tween tween = _meshRenderer.material.DOColor(Random.ColorHSV(), _duration);
            yield return new WaitWhile(() => tween.active);
        }
    }
}
