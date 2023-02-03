using UnityEngine;
using UnityEngine.Events;

public class PackingZone : Interactable
{
    [SerializeField] private PackingTable _table;
    [SerializeField] private Carrying _carrying;
    [SerializeField] private PackingPlace _playerPlace;

    private Player _player;

    public event UnityAction StartedPack;

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
        _table.StartPack(_player);
        _table.FinishedPack += OnFinishedPack;
    }

    private void OnFinishedPack(Item item)
    {
        FinishPack(item);
    }

    private void FinishPack(Item item)
    {
        _carrying.Put(item);
        _table.FinishedPack -= OnFinishedPack;
    }

    private void PutPlayerInPackingPlace()
    {
        _playerPlace.SetPositionAndRotation(transform, _player.transform);
        Animations.SetAnimatorParameter(ActionAnimator.PackingParameterHash);
    }

    private void PutItemOnTable()
    {
        Item item = _carrying.Drop();
        _table.Put(item);
    }
}
