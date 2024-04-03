using UnityEngine;

public class Magnifyer : MonoBehaviour
{
    [Min(0)] [SerializeField] private float _speed;

    void Update()
    {
        transform.localScale += Vector3.one * _speed * Time.deltaTime;
    }
}
