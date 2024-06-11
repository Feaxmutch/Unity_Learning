using UnityEngine;
using UnityEngine.UI;

public class HealthBar : HealthVisualizer
{
    [SerializeField] protected Slider _slider;

    protected override void ShowHealth()
    {
        _slider.value = GetNormmalizedHealth();
    }

    protected float GetNormmalizedHealth()
    {
        return Health.Value / Health.MaxValue;
    }
}
