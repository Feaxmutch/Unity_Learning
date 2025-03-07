using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider))]
public class Builder<T> : MonoBehaviour where T : Building
{
    private T _building;

    public event Action Builded;

    public void Initialize(T building)
    {
        _building = building;
    }

    public T Build()
    {
        T building = Instantiate(_building, transform.position, transform.rotation);
        gameObject.SetActive(false);
        Builded.Invoke();
        return building;
    }
}
