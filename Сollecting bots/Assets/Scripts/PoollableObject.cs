using System;
using UnityEngine;

public class PoollableObject : MonoBehaviour
{
    public event Action<PoollableObject> DeactivatedThis;
    public event Action Deactivated;

    private void OnDisable()
    {
        
    }

    public virtual void Reset() { }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        DeactivatedThis?.Invoke(this);
        Deactivated?.Invoke();
    }
}
