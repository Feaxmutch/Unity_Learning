using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private KeyCode _jump;
    [SerializeField] private KeyCode _moveLeft;
    [SerializeField] private KeyCode _moveRight;
    [SerializeField] private KeyCode _activateAbility;

    public event Action SendedJump;
    public event Action SendedLeft;
    public event Action SendedRight;
    public event Action SendedAbility;

    private void Update()
    {
        if (Input.GetKeyDown(_jump))
        {
            SendedJump?.Invoke();
        }
        else if(Input.GetKey(_moveLeft))
        {
            SendedLeft?.Invoke();
        }
        else if(Input.GetKey(_moveRight))
        {
            SendedRight?.Invoke();
        }
        else if (Input.GetKeyDown(_activateAbility))
        {
            SendedAbility?.Invoke();
        }
    }
}
