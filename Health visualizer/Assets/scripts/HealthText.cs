using TMPro;
using UnityEngine;

public class HealthText : HealthVisualizer
{
    [SerializeField] private TextMeshProUGUI _text;

    protected override void ShowHealth()
    {
        _text.SetText($"{Health.Value}/{Health.MaxValue}");
    }
}
