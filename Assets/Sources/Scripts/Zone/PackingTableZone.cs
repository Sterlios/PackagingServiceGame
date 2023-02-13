using UnityEngine;
using UnityEngine.Events;

public class PackingTableZone : TableZone, IPackable
{
    private Coroutine _packJob;
    private Item _updatingItem;

    public event UnityAction<Item> Packed;

    public void Pack()
    {
        _updatingItem = GetItem();
        _updatingItem.Packed += OnPacked;
        InteractableObject.BrokeAction += OnBrokenAction;

        if (_packJob == null)
            _packJob = StartCoroutine(_updatingItem.Pack());
    }

    private void OnPacked(Item item)
    {
        Packed?.Invoke(item);
        item.Packed -= OnPacked;
    }

    protected override void OnDropped(Item item)
    {
        if (!item.IsPacking)
            Put(item);
    }

    private void StopPack()
    {
        if (_packJob != null)
        {
            StopCoroutine(_packJob);
            _packJob = null;
        }

        if (!_updatingItem.IsPacking)
            Put(_updatingItem);

        InteractableObject.BrokeAction -= OnBrokenAction;
    }

    private void OnBrokenAction()
    {
        StopPack();
    }
}