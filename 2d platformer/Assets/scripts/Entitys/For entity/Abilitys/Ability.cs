using System;
using System.Collections;
using UnityEngine;

public abstract class Ability
{
    private Coroutine _activeLoop;

    public event Action Activated;
    public event Action CooldownSarted;
    public event Action CooldownEnded;

    public Ability(Entity owner, float cooldown, float duration, bool isOneShot)
    {
        Owner = owner;
        Cooldown = Mathf.Max(0, cooldown);
        Duration = Mathf.Max(0, duration);
        IsOneShot = isOneShot;
    }

    protected Entity Owner { get; }

    public float Cooldown { get; private set; }

    public float Duration { get; private set; }

    public bool IsOneShot { get; }

    public void Activate()
    {
        if (_activeLoop == null)
        {
            _activeLoop = Owner.StartCoroutine(ActiveLoop());
            Activated?.Invoke();
        }
    }

    protected abstract void OnActive();

    private IEnumerator ActiveLoop()
    {
        if (IsOneShot)
        {
            OnActive();
            yield return new WaitForSeconds(Duration);
        }
        else
        {
            float activateTime = Time.time;

            while (Time.time - activateTime < Duration)
            {
                OnActive();
                yield return null;
            }
        }

        Owner.StartCoroutine(StartCooldown());
        CooldownSarted?.Invoke();
    }

    private IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(Cooldown);
        _activeLoop = null;
        CooldownEnded?.Invoke();
    }
}
