using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BotStorage : MonoBehaviour
{
    [SerializeField] private BotSpawner _botSpawner;
    [SerializeField] private List<Bot> _bots;

    private BotFactory _botFactory;

    private void Awake()
    {
        _botFactory = new(_botSpawner);
    }

    public List<Bot> GetAllBots(Base @base)
    {
        List<Bot> bots = new();

        foreach (var bot in _bots)
        {
            if (bot.MainBase == @base)
            {
                bots.Add(bot);
            }
        }

        return bots;
    }

    public int GetBotsCount(Base @base)
    {
        return GetAllBots(@base).Count;
    }

    public List<Bot> GetSpecificBots(Base @base, bool isHoldingResource, bool isHaveTarget)
    {
        List<Bot> bots = GetAllBots(@base)
                         .Where(bot => bot.IsHoldingResource == isHoldingResource)
                         .Where(bot => bot.IsHaveTarget == isHaveTarget).ToList();
        return bots;
    }

    public bool TryCreateBot(Base baseOwner, Vector3 position)
    {
        bool isSuccessfull = _botFactory.TryCreateBot(position, out Bot bot);

        if (isSuccessfull)
        {
            bot.Initialize(baseOwner);
            _bots.Add(bot);
        }

        return isSuccessfull;
    }

    public bool ResourceIsReserved(Resource resource)
    {
        bool isMovingToResource;
        bool isHoldingResource;
        bool isReserved = false;

        foreach (var bot in _bots)
        {
            isMovingToResource = bot.CurrentTarget == resource.transform;
            isHoldingResource = bot.IsHoldingResource && bot.HoldedResource == resource;

            if (isMovingToResource || isHoldingResource)
            {
                isReserved = true;
                break;
            }
        }

        return isReserved;
    }
}
