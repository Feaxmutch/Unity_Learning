using UnityEngine;

public abstract class HealthVisualizer : MonoBehaviour
{
    [field : SerializeField] protected Health Health { get; private set; }

    private void Start()
    {
        ShowHealth();
    }

    private void OnEnable()
    {
        Health.TakedDamage += ShowHealth;
        Health.TakedHeal += ShowHealth;
    }
    private void OnDisable()
    {
        Health.TakedDamage -= ShowHealth;
        Health.TakedHeal -= ShowHealth;
    }

    protected abstract void ShowHealth();
}
