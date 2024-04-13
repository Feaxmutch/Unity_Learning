using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Shooter : MonoBehaviour
{
    [SerializeField] private float _number;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _shootDelay;
    [SerializeField] private Transform _objectToShoot;

    private void OnEnable()
    {
        StartCoroutine(_shootingWorker());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator _shootingWorker()
    {
        while (enabled)
        {
            var _vector3direction = (_objectToShoot.position - transform.position).normalized;
            var NewBullet = Instantiate(_prefab, transform.position + _vector3direction, Quaternion.identity);

            NewBullet.GetComponent<Rigidbody>().transform.up = _vector3direction;
            NewBullet.GetComponent<Rigidbody>().velocity = _vector3direction * _number;

            yield return new WaitForSeconds(_shootDelay);
        }
    }

    

    
}