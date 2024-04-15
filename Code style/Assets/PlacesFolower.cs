using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlacesFolower : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _placesParent;

    private List<Transform> _places = new();

    private void Start()
    {
        for (int i = 0; i < _placesParent.childCount; i++)
        {
            _places.Add(_placesParent.GetChild(i).GetComponent<Transform>());
        }
    }

    private void OnEnable()
    {
        StartCoroutine(Moving());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Moving()
    {
        while (enabled)
        {
            for (int i = 0; i < _places.Count; i++)
            {
                var currentPlace = _places[i];
                RotateToPlace(currentPlace);

                while (transform.position != currentPlace.position)
                {
                    transform.position = Vector3.MoveTowards(transform.position, currentPlace.position, _speed * Time.deltaTime);
                    yield return null;
                }
            }

            yield return null;
        }
    }

    public void RotateToPlace(Transform place)
    {
        var placePosition = place.position;
        transform.forward = placePosition - transform.position;
    }
}
