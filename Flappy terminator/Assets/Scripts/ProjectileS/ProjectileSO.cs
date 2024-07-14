using UnityEngine;

[CreateAssetMenu(fileName = "New projectile", menuName = "Scriptable Object/Projectile", order = 75)]
public class ProjectileSO : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; private set; }

    [field: SerializeField] public float Speed { get; private set; }
}
