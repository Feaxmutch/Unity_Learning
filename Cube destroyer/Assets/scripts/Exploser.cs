using UnityEngine;

public class Exploser : MonoBehaviour
{
    [SerializeField] private float _partsChance;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    public void Explose(RainbowCube rainbowCube)
    {
        if (CustomRandom.GetBoolean(_partsChance * GetScaleSum(rainbowCube.transform)))
        {
            rainbowCube.CreateParts();
        }

        Collider[] colliders = Physics.OverlapSphere(rainbowCube.transform.position, _explosionRadius);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out RainbowCube cube))
            {
                float newForce = _explosionForce / GetScaleSum(rainbowCube.transform);
                float newRadius = _explosionRadius / GetScaleSum(rainbowCube.transform);
                cube.GetComponent<Rigidbody>().AddExplosionForce(newForce, rainbowCube.transform.position, newRadius);
            }
        }

        Destroy(rainbowCube.gameObject);
    }

    private float GetScaleSum(Transform transform)
    {
        return transform.localScale.x + transform.localScale.y + transform.localScale.z;
    }
}

