using TMPro;
using UnityEngine;

public class InfoViewer<T> : MonoBehaviour where T : PoollableObject
{
    [SerializeField] private TextMeshProUGUI _textSpawned;
    [SerializeField] private TextMeshProUGUI _textCreated;
    [SerializeField] private TextMeshProUGUI _textActive;
    [SerializeField] private Spawner<T> _spawner;

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        if (_spawner.PoolCounter != null)
        {
            _spawner.SpawnedObjectsChanged += UpdateSpawned;
            _spawner.PoolCounter.CreatedCountChanged += UpdateCreated;
            _spawner.PoolCounter.ActiveCountChanged += UpdateActive;
        }
        else
        {
            _spawner.Initialized += Subscribe;
        }
    }

    private void Unsubscribe()
    {
        _spawner.SpawnedObjectsChanged -= UpdateSpawned;
        _spawner.PoolCounter.CreatedCountChanged -= UpdateCreated;
        _spawner.PoolCounter.ActiveCountChanged -= UpdateActive;
        _spawner.Initialized -= Subscribe;
    }

    private void UpdateSpawned()
    {
        _textSpawned.text = $"Заспавнено {_spawner.SpawnedObjects}";
    }

    private void UpdateCreated()
    {
        _textCreated.text = $"Созданно {_spawner.PoolCounter.CreatedObjects}";
    }

    private void UpdateActive()
    {
        _textActive.text = $"Активно {_spawner.PoolCounter.ActiveObjects}";
    }
}
