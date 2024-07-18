using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private KeyCode _jump;
    [SerializeField] private KeyCode _shoot;

    private Dictionary<KeyCode, bool> _sendedKeys = new();
    private Dictionary<KeyCode, bool> _fixedSendedKeys = new();
    private Dictionary<KeyCode, Action> _keyEvents = new();

    public KeyCode JumpKey { get => _jump; }

    public KeyCode ShootKey { get => _shoot; }

    private void Start()
    {
        _sendedKeys[_shoot] = false;
        _fixedSendedKeys[_jump] = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_jump))
        {
            _fixedSendedKeys[_jump] = true;
        }

        if (Input.GetKeyDown(_shoot))
        {
            _sendedKeys[_shoot] = true;
        }

        ApplyInput();
    }

    private void FixedUpdate()
    {
        ApplyFixedInput();
    }

    public void SubscribeToKey(KeyCode key, Action method)
    {
        if (_keyEvents.ContainsKey(key))
        {
            _keyEvents[key] += method;
        }
        else
        {
            _keyEvents[key] = method;
        }
    }

    public void UnsubscibeFromKey(KeyCode key, Action method)
    {
        if (_keyEvents.ContainsKey(key))
        {
            _keyEvents[key] -= method;
        }
    }

    private void ApplyInput()
    {
        List<KeyCode> keys = _sendedKeys.Keys.ToList();

        foreach (var key in keys)
        {
            if (_sendedKeys[key])
            {
                _keyEvents[key]?.Invoke();
                _sendedKeys[key] = false;
            }
        }
    }

    private void ApplyFixedInput()
    {
        List<KeyCode> keys = _fixedSendedKeys.Keys.ToList();

        foreach (var key in keys)
        {
            if (_fixedSendedKeys[key])
            {
                _keyEvents[key]?.Invoke();
                _fixedSendedKeys[key] = false;
            }
        }
    }
}
