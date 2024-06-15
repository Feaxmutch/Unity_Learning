using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerAnimatorHandler), typeof(SpriteRenderer), typeof(SpriteBlinker))]
[RequireComponent(typeof(BoxCollider2D), typeof(PlayerInput))]
public class Player : Entity
{
    [SerializeField] private ItemDetector _itemDetector;
    [SerializeField] private float _attackDamage;
    [SerializeField] private Eye _enemyFinder;

    private PlayerInput _playerInput;
    private SpriteBlinker _spriteBlinker;
    private BounceAttack _attack;
    private Color _damageColorMultiplyer = new Color(1, 1, 1, 0.5f);
    private Color _HealColorMultiplyer = new Color(1f, 2f, 1f, 1f);

    public event UnityAction ScoreChanged;

    public int Score { get; private set; } = 0;

    protected override void Awake()
    {
        base.Awake();
        _playerInput = GetComponent<PlayerInput>();
        _spriteBlinker = GetComponent<SpriteBlinker>();
        _enemyFinder.LookDirection = Vector2.down;
        _attack = new BounceAttack(_attackDamage, this, 70);
    }

    private void OnEnable()
    {
        Health.HealthIsOver += Death;
        Health.TakedDamage += DamageResponse;
        Health.TakedHeal += HealResponse;
        _itemDetector.ItemDetected += TryPickupItem;
        _playerInput.SendingJump += () => Mover.Jump();
        _playerInput.SendingLeft += () => Mover.Move(Vector2.left);
        _playerInput.SendingRight += () => Mover.Move(Vector2.right);
    }

    private void OnDisable()
    {
        Health.HealthIsOver -= Death;
        Health.TakedDamage -= DamageResponse;
        Health.TakedHeal -= HealResponse;
        _itemDetector.ItemDetected -= TryPickupItem;
        _playerInput.SendingJump -= () => Mover.Jump();
        _playerInput.SendingLeft -= () => Mover.Move(Vector2.left);
        _playerInput.SendingRight -= () => Mover.Move(Vector2.right);
    }

    private void Update()
    {
        if (_enemyFinder.TryFindComponent(out Enemy enemy))
        {
            _attack.DoAttack(enemy);
        }
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
            _spriteBlinker.TryBlink(_damageColorMultiplyer, 0.3f);
            yield return null;
        }
    }

    private void HealResponse()
    {
        _spriteBlinker.TryBlink(_HealColorMultiplyer, 0.5f);
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
}
