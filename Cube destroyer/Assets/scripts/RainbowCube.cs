using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(ColorRandomizer))]
[RequireComponent(typeof(Selector))]
[RequireComponent(typeof(Destroyer))]
public class RainbowCube: MonoBehaviour
{
    private void Awake()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        ColorRandomizer colorRandomizer = GetComponent<ColorRandomizer>();
        meshRenderer.material.color = colorRandomizer.Randomize();
        Selector selector = GetComponent<Selector>();
        selector.Initialize(meshRenderer.material);
    }
}
