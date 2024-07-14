using UnityEngine;

[CreateAssetMenu(fileName = "New ship", menuName = "Scriptable Object/Ship", order = 75)]
public class ShipSO : ScriptableObject
{
    [field : SerializeField] public Sprite Sprite { get; private set; }

    [field : SerializeField] public float Speed { get; private set; }
}
