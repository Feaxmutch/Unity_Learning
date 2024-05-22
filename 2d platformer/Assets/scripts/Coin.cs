using UnityEngine;

public class Coin : Item
{
    [field: SerializeField] public int ScoreValue { get; private set; }
}
