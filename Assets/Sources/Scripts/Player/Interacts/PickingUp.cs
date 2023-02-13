using UnityEngine;

class PickingUp : MonoBehaviour, IPickUpable
{
    [SerializeField] private float _pickingUpDistance;
    [SerializeField] private LayerMask _layerMask;
    
    private float _raycastHeight = 0.1f;

    public Item PickUp()
    {
        Vector3 startPoint = transform.position + transform.up * _raycastHeight;
        Vector3 direction = transform.forward;
        Ray ray = new Ray(startPoint, direction);

        Debug.DrawRay(startPoint, direction, Color.red);

        Item item = null;

        if (Physics.Raycast(ray, out RaycastHit hit, _pickingUpDistance, _layerMask))
            hit.collider.TryGetComponent(out item);

        return item;
    }
}

