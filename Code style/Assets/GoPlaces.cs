using UnityEngine;

public class GoPlaces : MonoBehaviour
{
    [SerializeField] private Transform _placesParent;

    private Transform[] _places;

    public float Float { get; private set; } //Speed?

    private void Start()
    {
        InitializePlaces();
    }

    private void Update()
    {
        foreach (var place in _places)
        {
            transform.position = Vector3.MoveTowards(transform.position, place.position, Float * Time.deltaTime);

            if (transform.position == place.position)
            {
                PlaceTakerLogic(place);
            }
        }
    }

    public Vector3 PlaceTakerLogic(Transform place)
    {
        var placePosition = place.transform.position;
        transform.forward = placePosition - transform.position;
        return placePosition;
    }

    private void InitializePlaces()
    {
        _places = new Transform[_placesParent.childCount];

        for (int i = 0; i < _places.Length; i++)
        {
            _places[i] = _placesParent.GetChild(i).GetComponent<Transform>();
        }
    }
}