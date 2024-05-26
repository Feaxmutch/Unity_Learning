using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerAnimatorHandler), typeof(SpriteRenderer), typeof(SpriteBlinker))]
[RequireComponent(typeof(BoxCollider2D), typeof(PlayerInput))]
public class Player : Entity
{
    [SerializeField] private ItemDetector _itemDetector;
    [SerializeField] private EntityDetector _entityDetector;
    [SerializeField] private int _attackDamage;

    private PlayerInput _playerInput;
    private SpriteBlinker _spriteBlinker;

    public event UnityAction ScoreChanged;

    public int Score { get; private set; } = 0;

    protected override void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _spriteBlinker = GetComponent<SpriteBlinker>();
        base.Awake();
    }

    private void OnEnable()
    {
        Health.HealthIsOver += Death;
        Health.TakedDamage += DamageResponse;
        Health.TakedHeal += HealResponse;
        _itemDetector.ItemIsDetected += TryPickupItem;
        _entityDetector.EntityIsDetected += TryAttack;
        _playerInput.SendingJump += () => _mover.Jump();
        _playerInput.SendingLeft += () => _mover.Move(Vector2.left);
        _playerInput.SendingRight += () => _mover.Move(Vector2.right);
    }

    private void OnDisable()
    {
        Health.HealthIsOver -= Death;
        Health.TakedDamage -= DamageResponse;
        Health.TakedHeal -= HealResponse;
        _itemDetector.ItemIsDetected -= TryPickupItem;
        _playerInput.SendingJump -= () => _mover.Jump();
        _playerInput.SendingLeft -= () => _mover.Move(Vector2.left);
        _playerInput.SendingRight -= () => _mover.Move(Vector2.right);
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    private void DamageResponse()
    {
        StartCoroutine(DamageBlink());
    }

    private IEnumerator DamageBlink()
    {
        while (Health.IsDamageResistance)
        {
            _spriteBlinker.TryBlink(new Color(1, 1, 1, 0.5f), 0.3f);
            yield return null;
        }
    }

    private void HealResponse()
    {
        _spriteBlinker.TryBlink(new Color(1f, 2f, 1f, 1f), 0.5f);
    }

    private void TryPickupItem(Item item)
    {
        bool pickupIsSuccessful = true;

        if (item is Coin)
        {
            Coin coin = item as Coin;
            Score += coin.ScoreValue;
            ScoreChanged?.Invoke();
        }
        else if (item is HealPotion)
        {
            HealPotion potion = item as HealPotion;
            Health.TakeHeal(potion.HealValue);
        }
        else
        {
            pickupIsSuccessful = false;
        }

        if (pickupIsSuccessful)
        {
            Destroy(item.gameObject);
        }
    }

    private void TryAttack(Entity entity)
    {
        if (entity is Enemy)
        {
            Enemy enemy = entity as Enemy;
            enemy.Health.TakeDamage(_attackDamage);
            _mover.Jump();
        }
    }
}
