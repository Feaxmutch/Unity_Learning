using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GroundDetector : MonoBehaviour
{
    private const int GroundLayer = 3;

    public bool OnGround { get; private set; } 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == GroundLayer)
        {
            OnGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == GroundLayer)
        {
            OnGround = false;
        }
    }

    private void OnValidate()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void Reset()
    {
        OnGround = false;
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
}
