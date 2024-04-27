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
        SpawnChilds(8, 8);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        
    }

    private void SpawnChilds(int count, int maxPositions)
    {
        List<List<List<GameObject>>> childs = new();
        int numberOfVectors = 3;
        int positionsInVector = (int)Mathf.Pow(maxPositions, 1f / numberOfVectors);

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
        bool positionIsFree = false;

        for (int i = 0; i < count; i++)
        {
            while (positionIsFree)
            {
                xPosition = Random.Range(0, childs.Count);
                yPosition = Random.Range(0, childs[xPosition].Count);
                zPosition = Random.Range(0, childs[yPosition].Count);
                positionIsFree = childs[xPosition][yPosition][zPosition] == null;
            }

            GameObject newObject = Instantiate(gameObject);
            newObject.transform.localScale = (gameObject.transform.localScale / positionsInVector);
            float x = xPosition * ((_mesh.bounds.size.x * newObject.transform.localScale.x) / positionsInVector) - (_mesh.bounds.size.x * newObject.transform.localScale.x) / 2 + ((_mesh.bounds.size.x * newObject.transform.localScale.x) / (positionsInVector * 2));
            float y = yPosition * ((_mesh.bounds.size.y * newObject.transform.localScale.y) / positionsInVector) - (_mesh.bounds.size.y * newObject.transform.localScale.y) / 2 + ((_mesh.bounds.size.y * newObject.transform.localScale.y) / (positionsInVector * 2));
            float z = zPosition * ((_mesh.bounds.size.z * newObject.transform.localScale.z) / positionsInVector) - (_mesh.bounds.size.z * newObject.transform.localScale.z) / 2 + ((_mesh.bounds.size.z * newObject.transform.localScale.z) / (positionsInVector * 2));
            Vector3 positionOffset = new(x, y, z);
            newObject.transform.position += positionOffset;
            childs[xPosition][yPosition][zPosition] = newObject;
        }

        GetComponent<Rigidbody>().AddExplosionForce(5000, gameObject.transform.position, 1000);
    }
}