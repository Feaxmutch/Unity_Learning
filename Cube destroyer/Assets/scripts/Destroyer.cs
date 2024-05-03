using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshFilter))]
public class Destroyer : MonoBehaviour
{
    [SerializeField] private int childsInVector;
    [SerializeField] private float _destroyChance;
    [SerializeField] private float _explosionForce; 
    [SerializeField] private float _explosionRadius; 

    private int _minCildsCount = 2;
    private int _maxCildsCount = 6;
    private int _childsCount;
    private int _clildsScaleDevide = 2;
    private Mesh _mesh;

    private void Awake()
    {
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        _mesh = meshFilter.mesh;
        _destroyChance /= _clildsScaleDevide;
    }

    private void OnMouseDown()
    {
        if (Random.Range(0f, 1f) < _destroyChance)
        {
            _childsCount = Random.Range(_minCildsCount, _maxCildsCount + 1);
            Explose(_childsCount, childsInVector);
        }

        Destroy(gameObject);
    }

    private void Explose(int count, int positionsInVector)
    {
        List<List<List<GameObject>>> childs = new();
        int numberOfVectors = 3;
        count = Mathf.Min(count, (int)Mathf.Pow(positionsInVector, numberOfVectors));

        for (int x = 0; x < positionsInVector; x++)
        {
            childs.Add(new());

            for (int y = 0; y < positionsInVector; y++)
            {
                childs[x].Add(new());

                for (int z = 0; z < positionsInVector; z++)
                {
                    childs[x][y].Add(null);
                }
            }
        }

        int xPosition = default;
        int yPosition = default;
        int zPosition = default;

        for (int i = 0; i < count; i++)
        {
            bool positionIsFree = false;

            while (positionIsFree == false)
            {
                xPosition = Random.Range(0, childs.Count);
                yPosition = Random.Range(0, childs[xPosition].Count);
                zPosition = Random.Range(0, childs[yPosition].Count);
                positionIsFree = childs[xPosition][yPosition][zPosition] == null;
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
            childs[xPosition][yPosition][zPosition] = newObject;
            Vector3 explosingPoint = transform.position + (transform.position - newObject.transform.position);
            newObject.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, explosingPoint, _explosionRadius);
        }
    }
}