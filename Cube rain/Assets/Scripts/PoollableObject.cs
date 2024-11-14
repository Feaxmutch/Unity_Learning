using System;
using UnityEngine;

public abstract class PoollableObject : MonoBehaviour
{
    public event Action<PoollableObject> Deactivated;

    public abstract void Reset();

    public void Deactivate()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            Deactivated?.Invoke(this);
        }
    }
}
