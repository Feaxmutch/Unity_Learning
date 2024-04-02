using UnityEngine;

public class Spiner : MonoBehaviour
{
    [SerializeField] private float _speed;

    void Update()
    {
        transform.Rotate(new(0, _speed * Time.deltaTime, 0));        
    }
}
