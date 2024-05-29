using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Selector))]
[RequireComponent(typeof(Destroyer))]
public class RainbowCube: MonoBehaviour
{
    private void Awake()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.color = Random.ColorHSV();
        Selector selector = GetComponent<Selector>();
        selector.Initialize(meshRenderer.material);
    }
}
