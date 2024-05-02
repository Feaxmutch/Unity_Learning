using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Destroyer : MonoBehaviour
{
    [SerializeField] private int childsCount;
    [SerializeField] private int childsInVector;
    [SerializeField] private float _defaultScale;

    private Mesh _mesh;

    private void Awake()
    {
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        _mesh = meshFilter.mesh;
    }

    private void OnMouseDown()
    {
        if (Random.Range(0, _defaultScale) < transform.localScale.x) //x - случайно выбранный вектор
        {
            Explose(childsCount, childsInVector);
            Destroy(gameObject);
        }
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

        int xPosition = 0;
        int yPosition = 0;
        int zPosition = 0;

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
            Vector3 finalScale = new Vector3(_mesh.bounds.size.x * transform.localScale.x, _mesh.bounds.size.y * transform.localScale.y, _mesh.bounds.size.z * transform.localScale.z);
            float xOffset = xPosition * (finalScale.x / positionsInVector) - finalScale.x / 2 + (finalScale.x / (positionsInVector * 2));
            float yOffset = yPosition * (finalScale.x / positionsInVector) - finalScale.x / 2 + (finalScale.x / (positionsInVector * 2));
            float zOffset = zPosition * (finalScale.x / positionsInVector) - finalScale.x / 2 + (finalScale.x / (positionsInVector * 2));
            Vector3 positionOffset = new(xOffset, yOffset, zOffset);
            newObject.transform.position += positionOffset;
            childs[xPosition][yPosition][zPosition] = newObject;
            newObject.GetComponent<Rigidbody>().AddExplosionForce(500, transform.position + (transform.position - newObject.transform.position), 100);
        }
    }
}