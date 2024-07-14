using UnityEngine;

public abstract class Initializable : MonoBehaviour
{
    protected virtual void Awake()
    {
        Initialize();
    }

    protected virtual void OnValidate()
    {
        Initialize();
    }

    protected abstract void Initialize();
}
