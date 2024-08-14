using System;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public event Action<Resource> Deactivated;

    private void OnDisable()
    {
        Deactivated?.Invoke(this);
    }
}
