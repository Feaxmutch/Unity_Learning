using UnityEngine;

[CreateAssetMenu(fileName = "new game balance", menuName = "Scriptable Object/Game balance", order = 51)]
public class GameBalance : ScriptableObject
{
    [Min(0)][SerializeField] private int _baseBotsLimit;
    [Min(0)][SerializeField] private int _botPrice;
    [Min(0)][SerializeField] private int _basePrice;
    [Min(0)][SerializeField] private float _resourcesSpawnDelay;
    [Min(0)][SerializeField] private float _minResourcesSpawnDelay;
    [Min(1)][SerializeField] private float _resourcesSpawnMultiplyer;

    public int BaseBotsLimit => _baseBotsLimit;
    public int BotPrice => _botPrice;
    public int BasePrice => _basePrice;
    public float ResourcesSpawnDelay => _resourcesSpawnDelay;
    public float MinResourcesSpawnDelay => _minResourcesSpawnDelay;
    public float ResourcesSpawnMultiplyer => _resourcesSpawnMultiplyer;

}
