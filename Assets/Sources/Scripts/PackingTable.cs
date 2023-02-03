using UnityEngine;
using UnityEngine.Events;

public class PackingTable : Storage
{
    [SerializeField] private PackingPlace _itemPlace;

    private Coroutine _packJob;
    private Player _player;
    public event UnityAction<Item> FinishedPack;

    public override void Put(Item item)
    {
        item.transform.parent = transform;

        _itemPlace.SetPositionAndRotation(transform, item.transform);

        base.Put(item);
    }

    public void StartPack(Player player)
    {
        if (_player == null)
        {
            _player = player;
            _player.Interapted += OnInterapted;
            CurrentItem.Packed += OnPacked;

            _packJob = StartCoroutine(CurrentItem.Pack());
        }
    }

    public void FinishPack()
    {
        _player.Interapted -= OnInterapted;
        CurrentItem.Packed -= OnPacked;

        if (_packJob != null)
            StopCoroutine(_packJob);

        if (CurrentItem.IsPacking)
            FinishedPack?.Invoke(Drop());

        _player = null;
    }

    private void OnPacked()
    {
        FinishPack();
    }

    private void OnInterapted()
    {
        FinishPack();
    }
}
