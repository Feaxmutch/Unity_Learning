using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClippingFinder
{
    public bool PositionIsFree(Vector3 position, MonoBehaviour reference)
    {
        BoxCollider[] objectColliders = GetAllBoxColliders(reference).ToArray();
        BoxCollider[] solidColliders = objectColliders.Where(collider => collider.isTrigger == false).ToArray();
        bool isHaveSolidCollider = solidColliders.Length > 0;
        bool isFree = true;

        if (isHaveSolidCollider)
        {
            foreach (var collider in solidColliders)
            {
                Vector3 colliderSize = collider.size.Multiply(collider.transform.localScale);
                Vector3 halfSize = colliderSize / 2;
                Collider[] colliders = Physics.OverlapBox(position + collider.center, halfSize, collider.transform.rotation);

                if (colliders.Where(collider => collider.isTrigger == false).Count() != 0)
                {
                    isFree = false;
                }
            }
        }

        return isFree;
    }

    private List<BoxCollider> GetAllBoxColliders(Component reference)
    {
        List<BoxCollider> colliders = reference.GetComponents<BoxCollider>().ToList();

        for (int i = 0; i < reference.transform.childCount; i++)
        {
            colliders.AddRange(GetAllBoxColliders(reference.transform.GetChild(i)));
        }

        return colliders;
    }
}
