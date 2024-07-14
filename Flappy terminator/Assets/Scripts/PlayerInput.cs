using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private KeyCode _jump;
    [SerializeField] private KeyCode _shoot;

    public event Action SendedJump;
    public event Action SendedShoot;

    private void Update()
    {
        if (Input.GetKeyDown(_jump))
        {
            SendedJump?.Invoke();
        }

        if (Input.GetKeyDown(_shoot))
        {
            SendedShoot?.Invoke();
        }
    }
}
