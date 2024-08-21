using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class WaitingArea : MonoBehaviour
{
    [Min(0)] [SerializeField] private int _maxBots;

    private List<Bot> _bots = new();

    public bool IsFree { get => BotsCount < _maxBots; }

    public int BotsCount { get => _bots.Count; }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Bot bot))
        {
            _bots.Add(bot);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent(out Bot bot))
        {
            _bots.Remove(bot);
        }
    }

    public bool IsInArea(Bot bot)
    {
        return _bots.Contains(bot);
    }
}
