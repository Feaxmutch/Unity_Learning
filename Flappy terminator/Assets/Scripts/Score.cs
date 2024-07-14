using System;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private EnemysGenerator _enemysGenerator;
    [SerializeField] private GameMode _gameMode;

    public event Action Changed;

    public int Value { get; private set; }

    private void OnEnable()
    {
        _enemysGenerator.EnemyDeactivated += AddValue;
        _gameMode.PreparedToStart += ResetValue;
    }

    private void OnDisable()
    {
        _enemysGenerator.EnemyDeactivated -= AddValue;
        _gameMode.PreparedToStart -= ResetValue;
    }

    private void AddValue()
    {
        Value++;
        Changed.Invoke();
    }

    private void ResetValue()
    {
        Value = 0;
        Changed.Invoke();
    }
}
