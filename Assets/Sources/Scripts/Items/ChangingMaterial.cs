using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ChangingMaterial : MonoBehaviour
{
    [SerializeField] private Material _unpacking;
    [SerializeField] private Material _packing;

    private MeshRenderer _currentMeshRenderer;
    private Item _item;

    private void Awake()
    {
        _currentMeshRenderer = GetComponent<MeshRenderer>();
        _currentMeshRenderer.material = _unpacking;
        _item = GetComponentInParent<Item>();
    }

    private void OnEnable()
    {
        _item.Packed += OnPacked;
    }

    private void OnDisable()
    {
        _item.Packed += OnPacked;
    }

    private void OnPacked(Item _)
    {
        ChangeMaterial();
    }

    private void ChangeMaterial()
    {
        _currentMeshRenderer.material = _packing;
    }
}

