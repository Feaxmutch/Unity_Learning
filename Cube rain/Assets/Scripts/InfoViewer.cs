using TMPro;
using UnityEngine;

public class InfoViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Spawner _spawner;

    private void Update()
    {
        _text.text = $"Заспавнено {_spawner.SpawnedObjectsCount} \n" +
                     $"Созданно {_spawner.Pool.CreatedObjectsCount} \n" +
                     $"Активно {_spawner.Pool.ActiveObjectsCount}";
    }
}
