using TMPro;
using UnityEngine;

public class ResourcesViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Base _base;

    private void OnEnable()
    {
        _base.ResourcesChanged += UpdateResourcesCount;
    }

    private void OnDisable()
    {
        _base.ResourcesChanged -= UpdateResourcesCount;
    }

    private void Start()
    {
        UpdateResourcesCount();
    }

    private void UpdateResourcesCount()
    {
        _text.SetText($"{_base.ResourcesCount}");
    }
}
