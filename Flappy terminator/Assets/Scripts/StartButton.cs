using System;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Button _button;
    [SerializeField] private GameMode _gameMode;

    private bool _isEnabled;

    public event Action Clicked;

    private void OnEnable()
    {
        _gameMode.Ended += Enable;
        _gameMode.Started += Disable;
        _button.onClick.AddListener(CallClick);
    }

    private void OnDisable()
    {
        _gameMode.Ended -= Enable;
        _gameMode.Started -= Disable;
        _button.onClick.RemoveListener(CallClick);
    }

    private void Enable()
    {
        _isEnabled = true;
        SetGroopActiving();
    }

    private void Disable()
    {
        _isEnabled = false;
        SetGroopActiving();
    }

    private void SetGroopActiving()
    {
        _canvasGroup.alpha = Convert.ToInt32(_isEnabled);
        _canvasGroup.interactable = _isEnabled;
    }

    private void CallClick()
    {
        Clicked.Invoke();
    }
}
