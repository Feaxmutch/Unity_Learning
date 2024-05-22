using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
   [Min(0)] [SerializeField] private int _maxValue;

    public event UnityAction HealthIsOver;

    public int Value { get; private set; }

    private void Start()
    {
        Value = _maxValue;
    }

    public void TakeDamage(int damage)
    {
        if (damage < Value)
        {
            Value -= damage;
        }
        else
        {
            Value = 0;
            HealthIsOver?.Invoke();
        }
    }
}
