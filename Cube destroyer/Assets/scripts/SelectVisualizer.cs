using UnityEngine;

public class SelectVisualizer : MonoBehaviour
{
    [SerializeField] private Selector _selector;

    private float _colorOffset = 0.9f;

    private void OnEnable()
    {
        _selector.CubeSelected += Select;
        _selector.CubeUnSelected += UnSelect;
    }

    private void OnDisable()
    {
        _selector.CubeSelected -= Select;
        _selector.CubeUnSelected -= UnSelect;
    }

    private void Select(RainbowCube rainbowCube)
    {
        rainbowCube.GetComponent<MeshRenderer>().material.color *= _colorOffset;
    }

    private void UnSelect(RainbowCube rainbowCube)
    {
        rainbowCube.GetComponent<MeshRenderer>().material.color /= _colorOffset;
    }
}
