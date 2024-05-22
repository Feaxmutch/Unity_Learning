using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorHandler : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Animator _animator;

    private void Awake()
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
        _animator.SetBool(PlayerAnimatorParametrs.IsFalling, !_player.OnGround);
    }

    private void Move()
    {
        _animator.SetBool(PlayerAnimatorParametrs.IsMooving, true);
    }

    private void Stop()
    {
        _animator.SetBool(PlayerAnimatorParametrs.IsMooving, false);
    }

    private void Jump()
    {
        _animator.SetTrigger(PlayerAnimatorParametrs.OnJump);
    }

    private static class PlayerAnimatorParametrs
    {
        public static readonly int IsMooving = Animator.StringToHash(nameof(IsMooving));
        public static readonly int IsFalling = Animator.StringToHash(nameof(IsFalling));
        public static readonly int OnJump = Animator.StringToHash(nameof(OnJump));
    }
}


