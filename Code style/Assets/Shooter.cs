using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private const int SecondsInMinute = 60;
    
    [SerializeField] private Rigidbody _bullet;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _roundPerMinute;
    [SerializeField] private Transform _target;

    private void OnEnable()
    {
        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        while (enabled)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            Rigidbody newBullet = Instantiate(_bullet, transform.position + direction, Quaternion.identity);
            newBullet.transform.up = direction;
            newBullet.velocity = direction * _bulletSpeed;

            yield return new WaitForSeconds(SecondsInMinute / _roundPerMinute);
        }
    }

    

    
}