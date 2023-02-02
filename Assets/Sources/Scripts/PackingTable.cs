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
        Vector3 position = _itemPlace.Position;

        for (Transform t = transform; t != null; t = t.parent)
            if (t.position != Vector3.zero)
                position = t.position + t.rotation * _itemPlace.Position;

        Quaternion rotation = Quaternion.Euler(_itemPlace.Rotation.eulerAngles + transform.rotation.eulerAngles);

        item.transform.SetPositionAndRotation(position, rotation);

        base.Put(item);
    }

    public Item Pack(Player player)
    {
        Item item = null;

        if (!_isPack)
        {
            _isPack = true; //после прерванного процесса упаковки, следующая упаковка работает не корректно
            _player = player;
            _player.Interapted += OnInterapted;
            _packJob = StartCoroutine(CurrentItem.Pack());

            if (CurrentItem.IsPacking)
            {
                if(_packJob != null)
                    StopCoroutine(_packJob);

                _player.Interapted -= OnInterapted;
                item = Drop();
            }
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
