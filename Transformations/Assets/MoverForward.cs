using UnityEngine;

public class MoverForward : MonoBehaviour
{
    [Min(0)] [SerializeField] private float _speed;

    void Update()
    {
        transform.Translate(new(0, 0, _speed * Time.deltaTime));
    }
}
