using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class RainbowCube: MonoBehaviour
{
    [SerializeField] private int _minPartsCount;
    [SerializeField] private int _maxPartsCount;

    private int _partsCount;
    private int _partsScaleDevide = 2;

    private void Awake()
    {
        GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
        _partsCount = Random.Range(_minPartsCount, _maxPartsCount + 1);
    }

    public void CreateParts()
    {
        for (int i = 0; i < _partsCount; i++)
        {
            RainbowCube newObject = Instantiate(this);
            newObject.transform.localScale = (transform.localScale / _partsScaleDevide);
        }
    }
}
