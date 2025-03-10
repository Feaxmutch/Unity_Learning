using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Base _selectedBase;
    private MouseTracker _mouseTracker;

    public event Action Selected;
    public event Action SelectReseted;

    public Vector3 PointOfSelect => _selectedBase.transform.position;
    public bool IsSelected => _selectedBase != null;

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
                if (IsSelected)
                {
                    _selectedBase.BaseBuilded -= ResetSelecting;
                }

                _selectedBase = @base;
                _selectedBase.BaseBuilded += ResetSelecting;
                Selected?.Invoke();
            }
            else
            {
                if (IsSelected)
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
        SelectReseted?.Invoke();
    }
}
