using UnityEngine;

public class SelectPointer : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private MeshRenderer _meshRenderer;

    private void OnEnable()
    {
        _player.Selected += EnableMesh;
        _player.Selected += SetPosition;
        _player.SelectReseted += DisableMesh;
    }

    private void OnDisable()
    {
        _player.Selected -= EnableMesh;
        _player.Selected -= SetPosition;
        _player.SelectReseted -= DisableMesh;
    }

    private void Start()
    {
        DisableMesh();
    }

    private void EnableMesh()
    {
        _meshRenderer.enabled = true;
    }

    private void DisableMesh()
    {
        _meshRenderer.enabled = false;
    }

    private void SetPosition()
    {
        transform.position = _player.PointOfSelect + _offset;
    }
}
