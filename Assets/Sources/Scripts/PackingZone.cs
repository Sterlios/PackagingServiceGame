using UnityEngine;

public class PackingZone : Interactable
{
    [SerializeField] private PackingTable _table;
    [SerializeField] private Carrying _carrying;

    private Player _player;
    private PackingPlace _packingPlace;

    private bool HasItemInHands => _carrying.Hands.HasItem;
    private bool HasItemOnTable => _table.Place.HasItem;
    private bool IsCurrentItemPacked => _carrying.Hands.CurrentItem?.IsPacking ?? false;

    private void Awake()
    {
        _packingPlace = GetComponentInChildren<PackingPlace>();
    }

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
        Item item = _packingPlace.Pack(_player, _table, Animations);
        _carrying.Put(item);
    }

    private void PutItemOnTable()
    {
        Item item = _carrying.Drop();
        _table.Put(item);
    }
}
