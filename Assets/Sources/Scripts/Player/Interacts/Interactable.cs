using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Storage))]
[RequireComponent(typeof(PickingUp))]
[RequireComponent(typeof(Player))]
public class Interactable : MonoBehaviour, IInteractable
{
    private Storage _storage;
    private PickingUp _pickingUp;
    private PackingTableZone _packingZone;
    private Player _player;

    public event UnityAction<Item> Dropped;
    public event UnityAction BrokeAction;

    private void Awake()
    {
        _storage = GetComponent<Storage>();
        _pickingUp = GetComponent<PickingUp>();
        _player = GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _packingZone = other.GetComponent<PackingTableZone>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PackingTableZone>(out _))
            _packingZone = null;
    }

    public void Interact()
    {
        Item item = null;

        if (_storage.HasItem)
        {
            item = _storage.Drop();
            Dropped?.Invoke(item);
        }
        else
        {
            if (_packingZone?.HasItem ?? false)
            {
                _packingZone.Packed += OnPacked;
                _player.BrokeAction += OnBrokeAction;
                _packingZone.Pack();
            }
            else
            {
                item = _pickingUp.PickUp();
            }

            _storage.Put(item);
        }
    }

    private void OnBrokeAction()
    {
        BrokeAction?.Invoke();
    }

    private void OnPacked(Item item)
    {
        _packingZone.Packed -= OnPacked;
        _storage.Put(item);
    }
}