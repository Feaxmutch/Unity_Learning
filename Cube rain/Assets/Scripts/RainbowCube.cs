using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class RainbowCube : PoollableObject
{
    [SerializeField] float _minLifeTime = 2f;
    [SerializeField] float _maxLifeTime = 5f;

    private Material _material;
    private Rigidbody _rigidbody;
    private bool _isToched = false;

    public Rigidbody Rigidbody { get => _rigidbody; }

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform _))
        {
            if (_isToched == false)
            {
                _isToched = true;
                _material.color = Random.ColorHSV();
                StartCoroutine(DeactivatingDelay());
            }
        }
    }

    public override void Reset()
    {
        _material.color = new Color(1f, 1f, 1f);
        _isToched = false;
        _rigidbody.velocity = Vector3.zero;
    }

    private IEnumerator DeactivatingDelay()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(_minLifeTime, _maxLifeTime));
        Deactivate();
    }
}