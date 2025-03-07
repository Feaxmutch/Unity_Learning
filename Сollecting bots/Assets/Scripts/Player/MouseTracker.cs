using System.Linq;
using UnityEngine;

public class MouseTracker 
{
    private Camera _mainCamera;

    public MouseTracker()
    {
        _mainCamera = Camera.main;
    }
    
    public Vector3 GetMousePositionInWorld()
    {
        return RaycastToMousePosition().point;
    }

    public GameObject GetObjectOnMousePosition()
    {
        return RaycastToMousePosition().collider.gameObject;
    }

    private RaycastHit RaycastToMousePosition()
    {
        const int FirstIndex = 0;

        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, float.MaxValue).OrderBy(hit => hit.distance).ToArray();
        RaycastHit[] solidHits = hits.Where(hit => hit.collider.isTrigger == false).ToArray();
        RaycastHit hit = new();

        if (solidHits.Length > 0)
        {
            hit = solidHits[FirstIndex];
        }

        return hit;
    }
}
