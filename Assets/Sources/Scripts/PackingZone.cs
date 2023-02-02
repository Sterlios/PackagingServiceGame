using UnityEngine;

public class PackingZone : Interactable
{
    [SerializeField] private PackingTable _table;
    [SerializeField] private Carrying _carrying;
    [SerializeField] private PackingPlace _playerPlace;

    private Player _player;

    private bool HasItemInHands => _carrying.Hands.HasItem;
    private bool HasItemOnTable => _table.HasItem;
    private bool IsCurrentItemPacked => _carrying.Hands.CurrentItem?.IsPacking ?? false;

    private void OnTriggerEnter(Collider other)
    {
        ActionAnimator actionAnimator = other.GetComponent<ActionAnimator>();


        if (actionAnimator != null)
            SetAnimator(actionAnimator);

        if(_player is null)
            _player = other.GetComponent<Player>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<ActionAnimator>(out _))
            SetAnimator(null);

        if (other.TryGetComponent<Player>(out _))
            _player = null;
    }

    public override void Interact()
    {
        if (HasItemInHands)
            HandleItemInHands();
        else
            HandleNoItemInHands();
    }

    private void HandleItemInHands()
    {
        if (HasItemOnTable || IsCurrentItemPacked)
        {
            DropItem();
        }
        else
        {
            PutItemOnTable();
            PackItem();
        }
    }

    private void DropItem()
    {
        _carrying.Drop();
    }

    private void HandleNoItemInHands()
    {
        if (HasItemOnTable)
            PackItem();
        else
            PickUpItem();
    }

    private void PickUpItem()
    {
        _carrying.PickUp();
    }

    private void PackItem()
    {
        PutPlayerInPackingPlace();

        Item item = _table.Pack(_player);
        _carrying.Put(item);
    }
    
    private void PutPlayerInPackingPlace()
    {
        Vector3 position = GetPlayerPosition();
        Quaternion rotation = Quaternion.Euler(_playerPlace.Rotation.eulerAngles + transform.rotation.eulerAngles);
        
        _player.transform.SetPositionAndRotation(position, rotation);
        Animations.SetAnimatorParameter(ActionAnimator.PackingParameterHash);
    }

    private Vector3 GetPlayerPosition()
    {
        Vector3 position = _playerPlace.Position;

        for (Transform t = transform; t != null; t = t.parent)
            if (t.position != Vector3.zero)
                position = t.position + t.rotation * _playerPlace.Position;

        return position;
    }

    private void PutItemOnTable()
    {
        Item item = _carrying.Drop();
        _table.Put(item);
    }
}
