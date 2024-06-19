using UnityEngine;
using UnityEngine.UI;

public class AbilityVisualizer : MonoBehaviour
{
    [SerializeField] private CanvasGroup _group;
    [SerializeField] private Image _ringImage;
    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _inactiveColor;

    private float _cooldownAlpha = 0.5f;

    private void Awake()
    {
        OnCooldown();
        OnStopCooldown();
    }

    public void Subscribe(Ability ability)
    {
        ability.Activated += OnActivate;
        ability.CooldownSarted += OnCooldown;
        ability.CooldownEnded += OnStopCooldown;
    }

    public void Unsibscribe(Ability ability)
    {
        ability.Activated -= OnActivate;
        ability.CooldownSarted -= OnCooldown;
        ability.CooldownEnded -= OnStopCooldown;
    }

    private void OnActivate()
    {
        _ringImage.color = _activeColor;
    }

    private void OnCooldown()
    {
        _ringImage.color = _inactiveColor;
        _group.alpha = _cooldownAlpha;
    }

    private void OnStopCooldown()
    {
        _group.alpha = 1;
    }
}
