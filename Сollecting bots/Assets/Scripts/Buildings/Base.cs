using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using System.Linq;

public class Base : Building
{
    private const int MinBots = 1;
    private const int FirstIndex = 0;

    [SerializeField] private BotStorage _botStotage;
    [SerializeField] private ResourcesScanner _scanner;
    [SerializeField] private GameBalance _gameBalance;
    [SerializeField] private PositionsRandomizer _positionsRandomizer;
    [SerializeField] private BaseBuilder _baseBuilder;
    [SerializeField] private Bot _botReference;

    private List<Transform> _resourcesPositions = new();
    private WaitForSeconds _commandsDelay = new(0.3f);
    private Behaviour _currentBehaviour;
    private ClippingFinder _clippingFinder = new();
    private int _resourcesCount;
    private Coroutine _commandsCorutine;
    
    
    public event Action ResourcesChanged;
    public event Action BaseBuilded;

    public int ResourcesCount 
    { 
        get => _resourcesCount; 
        private set
        {
            _resourcesCount = value;
            ResourcesChanged?.Invoke();
        }
    }

    private void Awake()
    {
        _baseBuilder.Initialize(this);
        _baseBuilder.gameObject.SetActive(false);
        _currentBehaviour = Behaviour.BuyngBots;
    }

    private void OnEnable()
    {
        _scanner.Founded += AddResourcePosition;
        ResourcesChanged += TryBuy;
        _baseBuilder.Builded += InvokeBaseBuilded;
        _commandsCorutine = StartCoroutine(CommandingTheBots());
    }

    private void OnDisable()
    {
        _scanner.Founded -= AddResourcePosition;
        ResourcesChanged -= TryBuy;
        _baseBuilder.Builded -= InvokeBaseBuilded;
        StopCoroutine(_commandsCorutine);
    }

    public void TakeResource(Resource resource)
    {
        resource.Deactivate();
        AddResource();
    }

    public void AddResource()
    {
        ResourcesCount++;
    }

    private void TryBuy()
    {
        switch (_currentBehaviour)
        {
            case Behaviour.BuyngBots:
                TryBuyBot();
                break;

            case Behaviour.BuyngBase:
                TryBuyBase();
                break;
        }
    }

    public void TryBuyBot()
    {
        bool isHaveResources = ResourcesCount >= _gameBalance.BotPrice;
        bool isBotsLimit = _botStotage.GetBotsCount(this) >= _gameBalance.BaseBotsLimit;
        int spawnAttemps = 10;

        if (isHaveResources && isBotsLimit == false)
        {
            if (TryFindFreePosition(spawnAttemps, out Vector3 position))
            {
                _botStotage.TryCreateBot(this, position);
                ResourcesCount -= _gameBalance.BotPrice;
            }
        }
    }

    public void TryBuyBase()
    {
        if (_botStotage.GetBotsCount(this) > MinBots)
        {
            if (ResourcesCount >= _gameBalance.BasePrice)
            {
                List<Bot> freeBots = _botStotage.GetSpecificBots(this, false, false);

                if (freeBots.Count > 0)
                {
                    freeBots[FirstIndex].MoveTo(_baseBuilder.transform);
                    _currentBehaviour = Behaviour.BuyngBots;
                    ResourcesCount -= _gameBalance.BasePrice;
                }
            }
        }
        else
        {
            TryBuyBot();
        }
    }

    public void BildNewBase(Vector3 position)
    {
        if (_clippingFinder.PositionIsFree(position, _baseBuilder))
        {
            if (_baseBuilder.gameObject.activeSelf == false)
            {
                _currentBehaviour = Behaviour.BuyngBase;
                _baseBuilder.gameObject.SetActive(true);
            }

            _baseBuilder.transform.position = position;
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
        RemoveReservedTargets();
        Transform[] targetsByDistance = _resourcesPositions.OrderBy(target => Vector3.Distance(target.position, bot.transform.position)).ToArray();

        if (_resourcesPositions.Count > 0)
        {
            Transform target = targetsByDistance[FirstIndex];
            _resourcesPositions.Remove(target);
            bot.MoveTo(target);
        }
    }

    private void SendMoveToBase(Bot bot)
    {
        bot.MoveTo(transform);
    }

    private IEnumerator CommandingTheBots()
    {
        while (enabled)
        {
            List<Bot> freeBots = _botStotage.GetSpecificBots(this, false, false);
            List<Bot> botsWdthResource = _botStotage.GetSpecificBots(this, true, false);

            foreach (var bot in freeBots)
            {
                _scanner.Scan();
                SendMoveToResource(bot);
                yield return null;
            }

            foreach (var bot in botsWdthResource)
            {
                SendMoveToBase(bot);
                yield return null;
            }

            yield return _commandsDelay;
        }
    }

    private void InvokeBaseBuilded()
    {
        BaseBuilded.Invoke();
    } 

    private void RemoveReservedTargets()
    {
        for (int i = _resourcesPositions.Count - 1; i >= 0; i--)
        {
            if (_resourcesPositions[i].TryGetComponent(out Resource resource))
            {
                if (_botStotage.ResourceIsReserved(resource))
                {
                    _resourcesPositions.RemoveAt(i);
                }
            }
        }
    }

    private bool TryFindFreePosition(int attempts, out Vector3 position)
    {
        int currentAttempt = 1;
        bool isFreePosition = false;
        position = Vector3.zero;

        while (currentAttempt <= attempts || isFreePosition == false)
        {
            position = _positionsRandomizer.GetPosition();
            isFreePosition = _clippingFinder.PositionIsFree(position, _botReference);
            currentAttempt++;
        }

        return isFreePosition;
    }

    private enum Behaviour
    {
        BuyngBots,
        BuyngBase
    }
}
