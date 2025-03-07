using System;
using UnityEngine;

public class PoollableObject : MonoBehaviour
{
    public event Action<PoollableObject> Deactivated;

    private void OnDisable()
    {
        Deactivated?.Invoke(this);
    }

    public virtual void Reset() { }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
