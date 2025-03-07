using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ResourceDisabler : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<SphereCollider>().isTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Resource resource))
        {
            resource.Deactivate();
        }
    }
}
