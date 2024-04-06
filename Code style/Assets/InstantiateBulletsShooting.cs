using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InstantiateBulletsShooting : MonoBehaviour
{
    [SerializeField] GameObject _prefab;
    [SerializeField] private float _timeWaitShooting;

    [field : SerializeField] public float Number { get; private set; }

    public Transform ObjectToShoot { get; private set; }

    private void Start()
    {
        StartCoroutine(_shootingWorker());
    }
    IEnumerator _shootingWorker()
    {
        bool isWork = enabled;

        while (isWork)
        {
            var direction = (ObjectToShoot.position - transform.position).normalized;
            var NewBullet = Instantiate(_prefab, transform.position + direction, Quaternion.identity);

            NewBullet.GetComponent<Rigidbody>().transform.up = direction;
            NewBullet.GetComponent<Rigidbody>().velocity = direction * Number;

            yield return new WaitForSeconds(_timeWaitShooting);
        }
    }
}