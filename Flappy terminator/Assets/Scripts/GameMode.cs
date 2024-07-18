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
        _player.Loosed += EndGame;
        _startButton.Clicked += OnClickToStart;
        Started += OnStarted;
        Ended += OnEnded;
    }

    private void OnDisable()
    {
        _player.Loosed -= EndGame;
        _startButton.Clicked -= OnClickToStart;
        Started -= OnStarted;
        Ended -= OnEnded;
    }

    private void Start()
    {
        StartGame();
        EndGame();
    }

    private void StartGame()
    {
        if (_isActive == false)
        {
            Started?.Invoke();
        }
    }

    private void EndGame()
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

    private void OnStarted()
    {
        _isActive = true;
    }

    private void OnEnded()
    {
        _isActive = false;
    }
}
