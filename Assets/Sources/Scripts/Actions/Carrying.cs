using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(ActionAnimator))]
[RequireComponent(typeof(Storage))]
class Carrying : Interactable
{
    [SerializeField] private float _pickingUpDistance;
    [SerializeField] private float _carryingDistance;
    [SerializeField] private float _carryingHeight;
    [SerializeField] private float _raycastAngle;
    [SerializeField] private LayerMask _layerMask;

    private float _raycastHeight = 0.1f;

    public Storage Hands { get; private set; }

    private void Awake()
    {
        SetAnimator(GetComponent<ActionAnimator>());
        Hands = GetComponent<Storage>();
    }

    public override void Interact()
    {
        if (Hands.HasItem)
            Drop();
        else 
            PickUp();
    }

    public void Put(Item item)
    {
        if (item != null)
        {
            Vector3 distance = transform.forward * _carryingDistance;
            Vector3 height = transform.up * _carryingHeight;

            item.transform.parent = transform;
            item.transform.position = transform.position + distance + height;
            item.GetComponent<Rigidbody>().isKinematic = true;
            Hands.Put(item);

            Animations.SetAnimatorParameter(ActionAnimator.CarryingParameterHash, true);
        }
    }

    public void PickUp()
    {
        Vector3 startPoint = transform.position + transform.up * _raycastHeight;

        Vector3[] directions = new Vector3[]
        {
                new Vector3(transform.forward.x, transform.forward.y, transform.forward.z),
                new Vector3(transform.forward.x, transform.forward.y, transform.forward.z + _raycastAngle),
                new Vector3(transform.forward.x, transform.forward.y, transform.forward.z - _raycastAngle),
                new Vector3(transform.forward.x, transform.forward.y + _raycastAngle, transform.forward.z),
                new Vector3(transform.forward.x, transform.forward.y - _raycastAngle, transform.forward.z),
                new Vector3(transform.forward.x, transform.forward.y + _raycastAngle, transform.forward.z - _raycastAngle),
                new Vector3(transform.forward.x, transform.forward.y - _raycastAngle, transform.forward.z - _raycastAngle),
                new Vector3(transform.forward.x, transform.forward.y + _raycastAngle, transform.forward.z + _raycastAngle),
                new Vector3(transform.forward.x, transform.forward.y - _raycastAngle, transform.forward.z + _raycastAngle)
        };

        foreach (var direction in directions)
        {
            Ray ray = new Ray(startPoint, direction);
            Debug.DrawRay(startPoint, direction, Color.red);

            if (TryGetItem(out Item item, ray))
            {
                Put(item);
                return;
            }
        }
    }

    public Item Drop()
    {
        Item item = Hands.Drop();

        if (item != null)
        {
            item.transform.parent = null;
            item.GetComponent<Rigidbody>().isKinematic = false;

            Animations.SetAnimatorParameter(ActionAnimator.CarryingParameterHash, false);
        }

        return item;
    }

    private bool TryGetItem(out Item item, Ray ray)
    {
        item = null;

        if (Physics.Raycast(ray, out RaycastHit hit, _pickingUpDistance, _layerMask))
            if (hit.collider.TryGetComponent(out item))
                return true;

        return false;
    }
}

