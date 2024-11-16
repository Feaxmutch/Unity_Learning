using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _spawner;

    private void OnEnable()
    {
        _spawner.ObjectSpawned += Subscribe;
    }

    private void OnDisable()
    {
        _spawner.ObjectSpawned -= Subscribe;
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
