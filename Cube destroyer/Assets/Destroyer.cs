using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(BoxCollider))]
public class Destroyer : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private Mesh _mesh;
    private Color _defaultColor;

    private void Start()
    {
        
    }

    private void Awake()
    {
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _defaultColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        _meshRenderer.material.color = _defaultColor;
        _mesh = meshFilter.mesh;
    }

    private void OnMouseEnter()
    {
        _meshRenderer.material.color = _defaultColor - new Color(0.1f, 0.1f, 0.1f);
    }

    private void OnMouseExit()
    {
        _meshRenderer.material.color = _defaultColor;
    }

    private void OnMouseDown()
    {
        SpawnChilds(8, 2);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        
    }

    private void SpawnChilds(int count, int positionsInVector)
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
            newObject.GetComponent<Rigidbody>().AddExplosionForce(500, transform.position, 1);
        }
    }
}