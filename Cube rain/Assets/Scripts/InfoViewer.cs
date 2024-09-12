using TMPro;
using UnityEngine;

public class InfoViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Spawner _spawner;

    private void Update()
    {
        _text.text = $"���������� {_spawner.SpawnedObjectsCount} \n" +
                     $"�������� {_spawner.Pool.CreatedObjectsCount} \n" +
                     $"������� {_spawner.Pool.ActiveObjectsCount}";
    }
}
