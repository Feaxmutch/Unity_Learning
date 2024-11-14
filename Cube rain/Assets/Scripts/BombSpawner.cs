using UnityEngine;

public class BombSpawner : Spawner
{
    [SerializeField] private Bomb _BombPrefab;
    [SerializeField] private CubeSpawner _spawner;

    protected override void OnEnable()
    {
        base.OnEnable();
        _spawner.ObjectSpawned += Subscribe;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _spawner.ObjectSpawned -= Subscribe;
    }

    protected override void Initialize()
    {
        SetPrefab(_BombPrefab);
        base.Initialize();
    }

    private void Subscribe(PoollableObject cube)
    {
        cube.Deactivated += SpawnBomb;
    }

    private void SpawnBomb(PoollableObject owner)
    {
        Spawn(owner.transform.position);
        owner.Deactivated -= SpawnBomb;
    }
}
