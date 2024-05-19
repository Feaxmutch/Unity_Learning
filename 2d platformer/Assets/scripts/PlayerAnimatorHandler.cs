using UnityEngine;

public class PlayerAnimatorHandler : MonoBehaviour
{
    private const string ParametrIsMooving = "IsMooving";
    private const string ParametrOnJump = "OnJump";
    private const string ParametrIsFalling = "IsFalling";

    [SerializeField] private Player _player;

    private Animator _animator;

    private void Start()
    {
        _animator = _player.GetComponent<Animator>();
        
    }

    private void OnEnable()
    {
        _player.OnMove += Move;
        _player.OnStop += Stop;
        _player.OnJump += Jump;
    }

    private void OnDisable()
    {
        _player.OnMove -= Move;
        _player.OnStop -= Stop;
        _player.OnJump -= Jump;
    }

    private void Update()
    {
        _animator.SetBool(ParametrIsFalling, !_player.OnGround);
    }

    private void Move()
    {
        _animator.SetBool(ParametrIsMooving, true);
    }

    private void Stop()
    {
        _animator.SetBool(ParametrIsMooving, false);
    }

    private void Jump()
    {
        _animator.SetTrigger(ParametrOnJump);
    }
}