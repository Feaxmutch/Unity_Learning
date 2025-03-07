using UnityEngine;

public class BotFactory
{
    private BotSpawner _botSpawner;

    public BotFactory(BotSpawner botSpawner)
    {
        _botSpawner = botSpawner;
    }

    public bool TryCreateBot(Vector3 position, out Bot createdBot)
    {
        return _botSpawner.TrySpawn(position, out createdBot);
    }
}
