using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using System.Linq;

public class Base : MonoBehaviour
{
    [SerializeField] private List<Bot> _bots;
    [SerializeField] private Bot _botPrefab;
    [SerializeField] private ResourcesScanner _scanner;
    [SerializeField] private Transform _botSpawnPoint;

    private List<Transform> _resourcesPositions = new();
    private WaitForSeconds _commandsDelay = new (0.2f);
    private int _botPrice = 3;
    private Vector3 _spawnAreaSize = Vector3.one;

    public event Action ResourcesChanged;

    public int ResourcesCount { get; private set; }

    private void OnEnable()
    {
        _scanner.Founded += AddResourcePosition;
        ResourcesChanged += TryBuyBot;

        foreach (var bot in _bots)
        {
            bot.GrappedResource += SendMoveToBase;
            bot.GivedResource += SendMoveToResource;
        }
    }

    private void OnDisable()
    {
        _scanner.Founded -= AddResourcePosition;
        ResourcesChanged -= TryBuyBot;

        foreach (var bot in _bots)
        {
            bot.GrappedResource -= SendMoveToBase;
            bot.GivedResource -= SendMoveToResource;
        }
    }

    private void Start()
    {
        StartCoroutine(CommandingTheIdleBots());
    }

    public void TakeResource(Resource resource)
    {
        ResourcesCount++;
        resource.gameObject.SetActive(false);
        ResourcesChanged.Invoke();
    }

    public void TryBuyBot()
    {
        if (ResourcesCount >= _botPrice)
        {
            Collider[] colliders = Physics.OverlapBox(_botSpawnPoint.position, _spawnAreaSize);

            if (colliders.Where(collider => collider.isTrigger == false).Count() == 0)
            {
                Bot newBot = Instantiate(_botPrefab, _botSpawnPoint.position, Quaternion.Euler(Vector3.zero));
                newBot.Initialize(this);
                _bots.Add(newBot);
            }

            ResourcesCount -= _botPrice;
            ResourcesChanged?.Invoke();
        }
    }

    private void AddResourcePosition(Transform transform)
    {
        if (_resourcesPositions.Contains(transform) == false)
        {
            _resourcesPositions.Add(transform);
        }
    }

    private void SendMoveToResource(Bot bot)
    {
        for (int i = 0; i < _bots.Count; i++)
        {
            if (_bots[i].CurrentTarget != null)
            {
                if (_resourcesPositions.Contains(_bots[i].CurrentTarget))
                {
                    _resourcesPositions.Remove(_bots[i].CurrentTarget);
                }
            }
        }

        if (_resourcesPositions.Count > 0)
        {
            Transform target = _resourcesPositions[0];
            _resourcesPositions.RemoveAt(0);
            bot.MoveTo(target);
        }
    }

    private void SendMoveToBase(Bot bot)
    {
        bot.MoveTo(transform);
    }

    private IEnumerator CommandingTheIdleBots()
    {
        while (enabled)
        {
            for (int i = _bots.Count - 1; i >= 0; i--)
            {
                if (_bots[i].IsHoldingResource == false && _bots[i].CurrentTarget == null)
                {
                    _scanner.Scan();
                    SendMoveToResource(_bots[i]);
                    yield return null;
                }

                if (_bots[i].IsHoldingResource == true)
                {
                    SendMoveToBase(_bots[i]);
                    yield return null;
                }
            }

            yield return _commandsDelay;
        }
    }
}
