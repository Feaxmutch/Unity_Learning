using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : IPoolCounter where T : PoollableObject
{
    private T _prefab;
    private Queue<T> _inactiveobjects = new();
    private List<T> _activeObjects = new();

    public event Action<T> Geted;
    public event Action<T> Released;
    public event Action<T> Created;
    public event Action ActiveCountChanged;
    public event Action CreatedCountChanged;

    public int ActiveObjects { get => _activeObjects.Count; }

    public int CreatedObjects { get => _inactiveobjects.Count + _activeObjects.Count; }

    public ObjectPool(T prefab)
    {
        _prefab = prefab;
    }

    public T Get()
    {
        if (_inactiveobjects.Count == 0)
        {
            Create();
        }

        T getedComponent = _inactiveobjects.Dequeue();
        getedComponent.Activate();
        _activeObjects.Add(getedComponent);
        Geted?.Invoke(getedComponent);
        ActiveCountChanged?.Invoke();
        getedComponent.Deactivated += Release;
        return getedComponent;
    }

    public void Release(T component)
    {
        if (_activeObjects.Contains(component))
        {
            component.Deactivated -= Release;
            _activeObjects.Remove(component);
            _inactiveobjects.Enqueue(component);
            Released?.Invoke(component);
            ActiveCountChanged?.Invoke();
        }
    }

    private void Release(PoollableObject component)
    {
        Release(component as T);
    }

    private void Create()
    {
        T newComponent = MonoBehaviour.Instantiate(_prefab);
        newComponent.Deactivate();
        _inactiveobjects.Enqueue(newComponent);
        Created?.Invoke(newComponent);
        CreatedCountChanged?.Invoke();
    }
}
