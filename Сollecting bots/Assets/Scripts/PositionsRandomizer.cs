using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PositionsRandomizer : MonoBehaviour 
{
    private BoxCollider _area;

    private Vector3 HalfAreaSize => _area.size / 2;

    private void Awake()
    {
        _area = GetComponent<BoxCollider>();
    }

    private void OnValidate()
    {
        _area = GetComponent<BoxCollider>();
        _area.isTrigger = true;
    }

    public Vector3 GetPosition()
    {
        return transform.position + Vector3.Scale(Random.insideUnitSphere, HalfAreaSize);
    }
}
