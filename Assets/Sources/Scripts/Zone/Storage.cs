using UnityEngine;
using UnityEngine.Events;

public class Storage : MonoBehaviour, IStorage
{
    [SerializeField] private StoragePlace _place;
    private Item _item;

    public bool HasItem => _item != null;
    protected bool IsItemPacked => _item?.IsPacking ?? false;

    public void Put(Item item)
    {
        if (!HasItem && item != null)
        {
            _item = item;
            item.transform.parent = _place.transform;
            item.transform.position = _place.transform.position;
            item.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public Item Drop()
    {
        Item item = _item;
        _item = null;

        item.transform.parent = null;
        item.GetComponent<Rigidbody>().isKinematic = false;

        return item;
    }
}