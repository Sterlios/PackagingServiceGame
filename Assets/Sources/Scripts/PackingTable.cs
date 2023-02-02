using UnityEngine;
using UnityEngine.Events;

public class PackingTable : Storage
{
    [SerializeField] private PackingPlace _itemPlace;

    private Coroutine _packJob;
    private Player _player;
    private bool _isPack = false;

    public override void Put(Item item)
    {
        item.transform.parent = transform;
        Vector3 position = _itemPlace.GetWorldPosition(transform);
        Quaternion rotation = _itemPlace.GetWorldRotation(transform);

        item.transform.SetPositionAndRotation(position, rotation);

        base.Put(item);
    }

    public Item Pack(Player player)
    {
        Item item = null;

        _player = player;
        _player.Interapted += OnInterapted;

        if (_packJob == null)
            _packJob = StartCoroutine(CurrentItem.Pack());

        if (CurrentItem.IsPacking)
        {
            if (_packJob != null)
                StopCoroutine(_packJob);

            _player.Interapted -= OnInterapted;
            item = Drop();
        }

        _isPack = false;

        return item;
    }

    private void OnInterapted(ActionAnimator actionAnimator)
    {
        if (_packJob != null)
            StopCoroutine(_packJob);

        actionAnimator.SetAnimatorParameter(ActionAnimator.InteraptParameterHash);
        _player.Interapted -= OnInterapted;
    }
}
