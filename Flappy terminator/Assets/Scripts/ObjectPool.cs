using System.Collections.Generic;
using System;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private T _prefab;
    private Queue<T> _objects = new();
    private List<T> _activeObjects = new();

    public event Action<T> Geted;
    public event Action<T> Released;
    public event Action<T> Created;

    public ObjectPool(T prefab)
    {
        _prefab = prefab;
    }

    public void Get()
    {
        if (_objects.Count == 0)
        {
            Create();
        }

        T getedComponent = _objects.Dequeue();
        getedComponent.gameObject.SetActive(true);
        _activeObjects.Add(getedComponent);
        Geted?.Invoke(getedComponent);
    }

    public void Release(T component)
    {
        if (_activeObjects.Contains(component))
        {
            _activeObjects.Remove(component);
            component.gameObject.SetActive(false);
            _objects.Enqueue(component);
            Released?.Invoke(component);
        }
    }

    public void ReleaseAll()
    {
        foreach (var activeObject in _activeObjects)
        {
            _objects.Enqueue(activeObject);
            activeObject.gameObject.SetActive(false);
            Released?.Invoke(activeObject);
        }

        _activeObjects.Clear();
    }

    private void Create()
    {
        T newComponent = MonoBehaviour.Instantiate(_prefab);
        newComponent.gameObject.SetActive(false);
        _objects.Enqueue(newComponent);
        Created?.Invoke(newComponent);
    }
}
