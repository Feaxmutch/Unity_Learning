using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Min(0)] [SerializeField] private float _maxValue;
    [Min(0)] [SerializeField] private float _resistanceTime;

    public event UnityAction HealthIsOver;
    public event UnityAction TakedDamage;
    public event UnityAction TakedHeal;

    public float MaxValue { get => _maxValue; }
    
    public float Value { get; private set; }

    public bool IsDamageResistance { get; private set; }

    private void Awake()
    {
        Value = _maxValue;
    }

    public void TakeDamage(float damage)
    {
        if (IsDamageResistance == false)
        {
            Value = Mathf.Max(0, Value - damage);
            StartCoroutine(ActivateResistance());
            TakedDamage?.Invoke();
        }

        if (Value == 0)
        {
            HealthIsOver?.Invoke();
        }
    }

    public void TakeHeal(float heal)
    {
        Value = Mathf.Min(_maxValue, Value + heal);
        TakedHeal?.Invoke();
    }

    private IEnumerator ActivateResistance()
    {
        IsDamageResistance = true;
        yield return new WaitForSeconds(_resistanceTime);
        IsDamageResistance = false;
    }
}