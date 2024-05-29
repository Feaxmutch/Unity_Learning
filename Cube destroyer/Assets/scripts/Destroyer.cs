using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(SphereCollider))]
public class Destroyer : MonoBehaviour
{
    [SerializeField] private int childsInVector;
    [SerializeField] private float _destroyChance;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private SphereCollider _explosionZone;

    private int _minCildsCount = 2;
    private int _maxCildsCount = 6;
    private int _childsCount;
    private int _clildsScaleDevide = 2;
    private Mesh _mesh;
    List<RainbowCube> _detectedCubes = new();

    private void Awake()
    {
        _detectedCubes.Clear();
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        _mesh = meshFilter.mesh;
        _destroyChance /= _clildsScaleDevide;
        UpdateExplosionZone();
    }

    private void OnValidate()
    {
        UpdateExplosionZone();
    }

    private void OnMouseDown()
    {
        if (Random.Range(0f, 1f) < _destroyChance)
        {
            _childsCount = Random.Range(_minCildsCount, _maxCildsCount + 1);
            ExploseWithParts(_childsCount, childsInVector);
        }
        else
        {
            ClearNullsAndDestroyed();
            Explose();
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out RainbowCube rainbowCube))
        {
            _detectedCubes.Add(rainbowCube);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out RainbowCube rainbowCube))
        {
            _detectedCubes.Remove(rainbowCube);
        }
    }

    private void UpdateExplosionZone()
    {
        _explosionZone.radius = _explosionRadius / GetSquare();
    }

    private float GetSquare()
    {
        return transform.localScale.x + transform.localScale.y + transform.localScale.z;
    }

    private void ExploseWithParts(int partsCount, int positionsInVector)
    {
        List<List<List<GameObject>>> parts = new();
        int numberOfVectors = 3;
        partsCount = Mathf.Min(partsCount, (int)Mathf.Pow(positionsInVector, numberOfVectors));

        for (int x = 0; x < positionsInVector; x++)
        {
            parts.Add(new());

            for (int y = 0; y < positionsInVector; y++)
            {
                parts[x].Add(new());

                for (int z = 0; z < positionsInVector; z++)
                {
                    parts[x][y].Add(null);
                }
            }
        }

        int xPosition = default;
        int yPosition = default;
        int zPosition = default;

        for (int i = 0; i < partsCount; i++)
        {
            bool positionIsFree = false;

            while (positionIsFree == false)
            {
                xPosition = Random.Range(0, parts.Count);
                yPosition = Random.Range(0, parts[xPosition].Count);
                zPosition = Random.Range(0, parts[yPosition].Count);
                positionIsFree = parts[xPosition][yPosition][zPosition] == null;
            }

            GameObject newObject = Instantiate(gameObject);
            newObject.transform.localScale = (gameObject.transform.localScale / positionsInVector);
            float xScale = _mesh.bounds.size.x * transform.localScale.x;
            float yScale = _mesh.bounds.size.y * transform.localScale.y;
            float zScale = _mesh.bounds.size.z * transform.localScale.z;
            float xOffset = xPosition * (xScale / positionsInVector) - xScale / 2 + (xScale / (positionsInVector * 2));
            float yOffset = yPosition * (yScale / positionsInVector) - yScale / 2 + (yScale / (positionsInVector * 2));
            float zOffset = zPosition * (zScale / positionsInVector) - zScale / 2 + (zScale / (positionsInVector * 2));
            Vector3 positionOffset = new(xOffset, yOffset, zOffset);
            newObject.transform.position += positionOffset;
            parts[xPosition][yPosition][zPosition] = newObject;
            Vector3 explosingPoint = transform.position + (transform.position - newObject.transform.position);
            newObject.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, explosingPoint, _explosionRadius);
        }
    }

    private void Explose()
    {
        foreach (var rainbowCube in _detectedCubes)
        {
            rainbowCube.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce / GetSquare(), transform.position, _explosionRadius);
        }
    }

    private void ClearNullsAndDestroyed()
    {
        for (int i = _detectedCubes.Count - 1; i > -1; i--)
        {
            if (_detectedCubes[i] == null || _detectedCubes[i].GetComponents<Component>().Length == 0)
            {
                _detectedCubes.RemoveAt(i);
            }
        }
    }
}