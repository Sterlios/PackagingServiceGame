using UnityEngine;
using UnityEngine.Events;

public class PackingTable : MonoBehaviour
{
    private Coroutine _packJob;
    private Player _player;
    private bool _isPack = false;

    public Storage Place { get; private set; }

    private void Awake()
    {
        Place = GetComponentInChildren<Storage>();
    }

    public void Put(Item item)
    {
        Place.Put(item);
        item.transform.parent = Place.transform;
        item.transform.SetPositionAndRotation(Place.transform.position, Place.transform.rotation);
    }

    public Item Pack(Player player)
    {
        Item item = Place.Drop();

        if (!_isPack)
        {
            _isPack = true; //после прерванного процесса упаковки, следующая упаковка работает не корректно
            _player = player;
            _player.Interapted += OnInterapted;
            _packJob = StartCoroutine(item.Pack());

            if (!item.IsPacking)
            {
                Place.Put(item);
                item = null;
            }
        }

        _isPack = false;

        return item;
    }

    private void OnInterapted(ActionAnimator actionAnimator)
    {
        StopCoroutine(_packJob);
        actionAnimator.SetAnimatorParameter(ActionAnimator.InteraptParameterHash);
        _player.Interapted -= OnInterapted;
    }
}
