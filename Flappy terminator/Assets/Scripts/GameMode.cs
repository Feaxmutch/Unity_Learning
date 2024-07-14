using System;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private StartButton _startButton;

    private bool _isActive = false;

    public event Action Ended;
    public event Action PreparedToStart;
    public event Action Started;

    private void OnEnable()
    {
        _player.Deactivated += EndGame;
        _startButton.Clicked += OnClickToStart;
        Started += () => _isActive = true;
        Ended += () => _isActive = false;
    }

    private void OnDisable()
    {
        _player.Deactivated -= EndGame;
        _startButton.Clicked -= OnClickToStart;
        Started -= () => _isActive = true;
        Ended -= () => _isActive = false;
    }

    private void Start()
    {
        StartGame();
        EndGame(_player);
    }

    private void StartGame()
    {
        if (_isActive == false)
        {
            Started?.Invoke();
        }
    }

    private void EndGame(Ship _)
    {
        if (_isActive == true)
        {
            Ended?.Invoke();
            PreparedToStart?.Invoke();
        }
    }

    private void OnClickToStart()
    {
        StartGame();
    }
}
