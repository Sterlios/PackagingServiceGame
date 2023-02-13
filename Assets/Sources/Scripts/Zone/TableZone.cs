using UnityEngine;

[RequireComponent(typeof(Storage))]
public class TableZone : MonoBehaviour
{
    private Storage _storage;
    private Interactable _interactable;

    protected Interactable InteractableObject => _interactable;

    public bool HasItem => _storage.HasItem;

    private void Awake()
    {
        _storage = GetComponent<Storage>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out _interactable))
            InteractableObject.Dropped += OnDropped;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Interactable>(out _))
        {
            _interactable.Dropped -= OnDropped;
            _interactable = null;
        }
    }

    protected Item GetItem()
    {
        return _storage.Drop();
    }

    protected void Put(Item item)
    {
        _storage.Put(item);
    }

    protected virtual void OnDropped(Item item)
    {
        if(item.IsPacking)
            Put(item);
    }
}