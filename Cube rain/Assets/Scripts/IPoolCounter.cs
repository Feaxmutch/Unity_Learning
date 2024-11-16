using System;

public interface IPoolCounter
{
    public event Action ActiveCountChanged;
    public event Action CreatedCountChanged;

    public int ActiveObjects { get; }

    public int CreatedObjects { get; }
}
