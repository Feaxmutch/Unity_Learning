using System.Collections.Generic;
using System;
using UnityEngine;

public class ObjectPool<T> where T : PoollableObject
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

    public T Get()
    {
        if (_objects.Count == 0)
        {
            Create();
        }

        T getedComponent = _objects.Dequeue();
        getedComponent.Activate();
        getedComponent.DeactivatedThis += Release;
        _activeObjects.Add(getedComponent);
        getedComponent.Reset();
        Geted?.Invoke(getedComponent);
        return getedComponent;
    }

    private void Release(PoollableObject poollableObject)
    {
        if (poollableObject is T)
        {
            T releasingObject = poollableObject as T;

            if (_activeObjects.Contains(releasingObject))
            {
                _activeObjects.Remove(releasingObject);
                releasingObject.DeactivatedThis -= Release;
                releasingObject.Deactivate();
                _objects.Enqueue(releasingObject);
                Released?.Invoke(releasingObject);
            }
        }
    }

    private void Create()
    {
        T newComponent = MonoBehaviour.Instantiate(_prefab);
        newComponent.Deactivate();
        _objects.Enqueue(newComponent);
        Created?.Invoke(newComponent);
    }
}
