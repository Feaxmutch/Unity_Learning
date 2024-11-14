using System;

public interface IInitializeble
{
    public event Action Initialized;

    protected virtual void Initialize()
    {
        
    }
}
