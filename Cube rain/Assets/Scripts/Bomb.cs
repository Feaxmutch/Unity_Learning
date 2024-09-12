using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(MeshRenderer))]
public class Bomb : PoollableObject
{
    [SerializeField] private float _exploseRadius;
    [SerializeField] private float _exploseForce;
    [SerializeField] private float _minExploseDelay;
    [SerializeField] private float _maxExploseDelay;

    private float _exploseDelay;
    private WaitForSeconds _delay;
    private Material _material;

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
    }

    private void OnEnable()
    {
        _exploseDelay = Random.Range(_minExploseDelay, _maxExploseDelay);
        _delay = new(_exploseDelay);
        StartCoroutine(StartExplosion());
    }

    public override void Reset()
    {
        _material.color = new(_material.color.r, _material.color.g, _material.color.b, 1);
    }

    private void Explose()
    {
        List<RainbowCube> cubes = new();
        Collider[] cubeColliders = Physics.OverlapSphere(transform.position, _exploseRadius).
            Where(collider => collider.TryGetComponent(out RainbowCube _)).ToArray();

        foreach (var collider in cubeColliders)
        {
            RainbowCube cube = collider.GetComponent<RainbowCube>();
            cube.Rigidbody.AddExplosionForce(_exploseForce, transform.position, _exploseRadius);
        }

        Deactivate();
    }

    private IEnumerator StartExplosion()
    {
        Color newColor = new(_material.color.r, _material.color.g, _material.color.b, 0);
        _material.DOColor(newColor, _exploseDelay).SetEase(Ease.InCirc);
        yield return _delay;
        Explose();
    }
}
