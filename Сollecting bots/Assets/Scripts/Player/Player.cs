using UnityEngine;

public class Player : MonoBehaviour
{
    private Base _selectedBase;
    private MouseTracker _mouseTracker;

    private void Awake()
    {
        _mouseTracker = new();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject clickedObject = _mouseTracker.GetObjectOnMousePosition().transform.root.gameObject;

            if (clickedObject.TryGetComponent(out Base @base))
            {
                if (_selectedBase != null)
                {
                    _selectedBase.BaseBuilded -= ResetSelecting;
                }

                _selectedBase = @base;
                _selectedBase.BaseBuilded += ResetSelecting;
            }
            else
            {
                if (_selectedBase != null)
                {
                    _selectedBase.BildNewBase(_mouseTracker.GetMousePositionInWorld());
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            ResetSelecting();
        }
    }

    private void ResetSelecting()
    {
        _selectedBase.BaseBuilded -= ResetSelecting;
        _selectedBase = null;
    }
}
