using UnityEngine;

[RequireComponent(typeof(PathFolower))]
[RequireComponent(typeof(Animator))]
public class Thief : MonoBehaviour
{
    private const string SpeedParametr = "Speed";

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed;

    private PathFolower _pathFolower;
    private Animator _animator;

    private void Start()
    {
        _pathFolower = GetComponent<PathFolower>();
        _animator = GetComponent<Animator>();
        SetSpeed(_movementSpeed, _rotationSpeed);
        _pathFolower.StartMoving(false, 0);
    }

    private void SetSpeed(float movement, float rotation)
    {
        _movementSpeed = movement;
        _rotationSpeed = rotation;
        _pathFolower.SetMovementSpeed(_movementSpeed);
        _pathFolower.SetRotationSpeed(_rotationSpeed);
        _animator.SetFloat(SpeedParametr, _movementSpeed);
    }
}
