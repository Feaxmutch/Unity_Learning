using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlacesFollower : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _placesParent;

    private Transform[] _places;

    private void Start()
    {
        _places = new Transform[_placesParent.childCount];

        for (int i = 0; i < _placesParent.childCount; i++)
        {
            _places[i] = (_placesParent.GetChild(i));
        }

        StartCoroutine(Moving());
    }

    private IEnumerator Moving()
    {
        while (enabled)
        {
            foreach (var place in _places)
            {
                RotateToPlace(place);

                while (transform.position != place.position)
                {
                    transform.position = Vector3.MoveTowards(transform.position, place.position, _speed * Time.deltaTime);
                    yield return null;
                }
            }

            yield return null;
        }
    }

    public void RotateToPlace(Transform place)
    {
        Vector3 placePosition = place.position;
        transform.forward = placePosition - transform.position;
    }
}
