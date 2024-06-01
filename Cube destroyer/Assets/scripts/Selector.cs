using UnityEngine;

public class Selector : MonoBehaviour
{
    [SerializeField] private Exploser _destroyer;
    [SerializeField] private Camera _camera;

    private RainbowCube _currentSelect;

    public event CubeAction CubeSelected;
    public event CubeAction CubeUnSelected;

    private void Update()
    {
        SelectCube();

        if (_currentSelect != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _destroyer.Explose(_currentSelect);
            }
        }
    }

    private void SelectCube()
    {
        RaycastHit[] hits = Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition), 1000);
        bool cubeIsUnSelected = true;

        if (_currentSelect == null)
        {
            foreach (var hit in hits)
            {
                if (hit.collider.TryGetComponent(out RainbowCube rainbowCube))
                {
                    _currentSelect = rainbowCube;
                    CubeSelected?.Invoke(_currentSelect);
                }
            }
        }
        else
        {
            foreach (var hit in hits)
            {
                if (hit.collider.TryGetComponent(out RainbowCube rainbowCube))
                {
                    if (rainbowCube == _currentSelect)
                    {
                        cubeIsUnSelected = false;
                    }
                }
            }

            if (cubeIsUnSelected)
            {
                CubeUnSelected?.Invoke(_currentSelect);
                _currentSelect = null;
            }
        }
    }
}

public delegate void CubeAction(RainbowCube rainbowCube);
