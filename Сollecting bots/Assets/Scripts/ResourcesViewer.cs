using TMPro;
using UnityEngine;

public class ResourcesViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Base _base;
    [SerializeField] private Vector3 _positionOffset;

    private void OnEnable()
    {
        _base.ResourcesChanged += ViewResourcesCount;
    }

    private void OnDisable()
    {
        _base.ResourcesChanged -= ViewResourcesCount;
    }

    private void Start()
    {
        ViewResourcesCount();
        MoveTextToBase();
    }

    private void ViewResourcesCount()
    {
        _text.SetText($"{_base.ResourcesCount}");
    }

    private void MoveTextToBase()
    {
        Vector3 basePosition = Camera.main.WorldToScreenPoint(_base.transform.position);
        _text.rectTransform.position = basePosition + _positionOffset;
    }
}
