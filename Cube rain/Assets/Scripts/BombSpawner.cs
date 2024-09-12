using UnityEngine;

public class BombSpawner : Spawner
{
    [SerializeField] private Bomb _BombPrefab;
    [SerializeField] private CubeSpawner _spawner;

    protected override void Awake()
    {
        SetPrefab(_BombPrefab);
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _spawner.CubeSpawned += Subscribe;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _spawner.CubeSpawned -= Subscribe;
    }

    private void Subscribe(RainbowCube cube)
    {
        cube.Deactivated += SpawnBomb;
    }

    private void SpawnBomb(PoollableObject owner)
    {
        Spawn(owner.transform.position);
        owner.Deactivated -= SpawnBomb;
    }
}
