using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Player : MonoBehaviour , IDamageble
{
    [SerializeField] private int _health;
    [SerializeField] private ContactFilter2D _contactFilter2D;

    private Movement _movement;

    private bool OnGround { get; set; }

    private void Start()
    {
        _movement = GetComponent<Movement>();       
    }

    private void Update()
    {
        ApplyInput();

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 3)
        {
            OnGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 3)
        {
            OnGround = false;
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
    }

    private void ApplyInput() 
    {
        if (Input.GetKeyDown(KeyCode.Space) && OnGround)
        {
            _movement.Jump();
        }

        if (Input.GetKey(KeyCode.D))
        {
            _movement.MoveRight();
        }

        if (Input.GetKey(KeyCode.A))
        {
            _movement.MoveLeft();
        }
    }
}
