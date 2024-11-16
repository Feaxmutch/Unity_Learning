using System;
using UnityEngine;

public abstract class PoollableObject : MonoBehaviour
{
    public event Action<PoollableObject> Deactivated;

    public abstract void Reset();

    public void Activate()
    {
        if (gameObject.activeSelf == false)
        {
            Reset();
            gameObject.SetActive(true);
        }
        else
        {
            throw new InvalidOperationException("gameObject allready active");
        }
    }

    public void Deactivate()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            Deactivated?.Invoke(this);
        }
        else
        {
            throw new InvalidOperationException("gameObject allready inactive");
        }
    }
}
