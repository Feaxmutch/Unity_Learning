using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(BoxCollider))]
public class Selector : MonoBehaviour
{
    private Material _material;
    private Color _defaultColor;
    private float _colorOffset = 0.9f;

    public void Initialize(Material material)
    {
        _material = material;
        _defaultColor = _material.color;
    }

    private void OnMouseEnter()
    {
        _material.color = _defaultColor * _colorOffset;
    }

    private void OnMouseExit()
    {
        _material.color = _defaultColor;
    }
}
