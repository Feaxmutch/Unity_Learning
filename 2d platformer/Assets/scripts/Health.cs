using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Min(0)] [SerializeField] private int _maxValue;
    [Min(0)] [SerializeField] private float _resistanceTime;

    public event UnityAction HealthIsOver;
    public event UnityAction TakedDamage;
    public event UnityAction TakedHeal;

    public int Value { get; private set; }

    public bool IsDamageResistance { get; private set; }

    private void Start()
    {
        Value = _maxValue;
    }

    public void TakeDamage(int damage)
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

    public void TakeHeal(int heal)
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
