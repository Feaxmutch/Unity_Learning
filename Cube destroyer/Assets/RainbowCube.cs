using UnityEngine;

[RequireComponent(typeof(ColorRandomizer))]
[RequireComponent(typeof(Selector))]
[RequireComponent(typeof(Destroyer))]
[RequireComponent(typeof(MeshRenderer))]
public class RainbowCube : MonoBehaviour
{
    ColorRandomizer _colorRandomizer;
    Selector _selector;
    Material _material;

    private void Awake()
    {
        _colorRandomizer = GetComponent<ColorRandomizer>();
        _selector = GetComponent<Selector>();
        _material = GetComponent<MeshRenderer>().material;
        _material.color = _colorRandomizer.Randomize();
        _selector.Initialize(_material);
    }
}
