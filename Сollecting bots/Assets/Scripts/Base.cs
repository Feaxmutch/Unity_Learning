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

    private List<Resource> _resources = new();
    private Queue<Vector3> _resourcesPositions = new();
    private WaitForSeconds _commandsDelay = new (0.2f);
    private WaitForSeconds _scanningDelay = new (5);
    private int _botPrice = 3;
    private Vector3 _spawnAreaSize = Vector3.one;


    public event Action ResourcesChanged;

    public int ResourcesCount { get => _resources.Count; }

    private void OnEnable()
    {
        _scanner.Founded += AddResourcePosition;
        ResourcesChanged += TryBuyBot;

        foreach (var bot in _bots)
        {
            bot.GrappedResource += SendMoveToBase;
            bot.DroppedResource += SendMoveToResource;
        }
    }

    private void OnDisable()
    {
        _scanner.Founded -= AddResourcePosition;
        ResourcesChanged -= TryBuyBot;


        foreach (var bot in _bots)
        {
            bot.GrappedResource -= SendMoveToBase;
            bot.DroppedResource -= SendMoveToResource;
        }
    }

    private void Start()
    {
        StartCoroutine(CommandingTheIdleBots());
        StartCoroutine(ScanningResources());
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Resource resource))
        {
            TakeResource(resource);
        }
    }

    public void TakeResource(Resource resource)
    {
        _resources.Add(resource);
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
                _bots.Add(Instantiate(_botPrefab, _botSpawnPoint.position, Quaternion.Euler(Vector3.zero)));
            }

            _resources.RemoveRange(0, _botPrice);
            ResourcesChanged?.Invoke();
        }
    }

    private void AddResourcePosition(Vector3 position)
    {
        if (_resourcesPositions.Contains(position) == false)
        {
            _resourcesPositions.Enqueue(position);
        }
    }

    private void SendMoveToResource(Bot bot)
    {
        if (_resourcesPositions.Count > 0)
        {
            bot.MoveTo(_resourcesPositions.Dequeue());
        }
    }

    private void SendMoveToBase(Bot bot)
    {
        bot.MoveTo(transform.position);
    }

    private IEnumerator CommandingTheIdleBots()
    {
        while (enabled)
        {
            for (int i = _bots.Count - 1; i >= 0; i--)
            {
                if (_bots[i].IsHoldingResource == false && _bots[i].IsMooving == false)
                {
                    SendMoveToResource(_bots[i]);
                    yield return null;
                }

                if (_bots[i].IsHoldingResource == true && _bots[i].IsMooving == false)
                {
                    SendMoveToBase(_bots[i]);
                    yield return null;
                }
            }

            yield return _commandsDelay;
        }
    }

    private IEnumerator ScanningResources()
    {
        while (enabled)
        {
            _scanner.Scan();
            yield return _scanningDelay;
        }
    }
}
