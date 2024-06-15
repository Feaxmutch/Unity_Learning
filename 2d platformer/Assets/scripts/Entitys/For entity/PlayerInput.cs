using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private KeyCode _jump;
    [SerializeField] private KeyCode _moveLeft;
    [SerializeField] private KeyCode _moveRight;

    public event UnityAction SendingJump;
    public event UnityAction SendingLeft;
    public event UnityAction SendingRight;

    private void Update()
    {
        if (Input.GetKeyDown(_jump))
        {
            SendingJump?.Invoke();
        }
        else if(Input.GetKey(_moveLeft))
        {
            SendingLeft?.Invoke();
        }
        else if(Input.GetKey(_moveRight))
        {
            SendingRight?.Invoke();
        }
    }
}
