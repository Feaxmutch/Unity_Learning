using TMPro;
using UnityEngine;

public class InfoViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textSpawned;
    [SerializeField] private TextMeshProUGUI _textCreated;
    [SerializeField] private TextMeshProUGUI _textActive;
    [SerializeField] private Spawner _spawner;

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
        if (_spawner.Pool != null)
        {
            _spawner.SpawnedObjectsChanged += UpdateSpawned;
            _spawner.Pool.CreatedCountChanged += UpdateCreated;
            _spawner.Pool.ActiveCountChanged += UpdateActive;
        }
        else
        {
            _spawner.Initialized += Subscribe;
        }
    }

    private void Unsubscribe()
    {
        _spawner.SpawnedObjectsChanged -= UpdateSpawned;
        _spawner.Pool.CreatedCountChanged -= UpdateCreated;
        _spawner.Pool.ActiveCountChanged -= UpdateActive;
    }

    private void UpdateSpawned()
    {
        _textSpawned.text = $"���������� {_spawner.SpawnedObjects}";
    }

    private void UpdateCreated()
    {
        _textCreated.text = $"�������� {_spawner.Pool.CreatedObjects}";
    }

    private void UpdateActive()
    {
        _textActive.text = $"������� {_spawner.Pool.ActiveObjects}";
    }
}
