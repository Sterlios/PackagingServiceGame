
class PatrolTransition : BaseTransition
{
    private PlayerDetect _playerDetect;
    private ItemDetect _itemDetect;

    private void Start()
    {
        _playerDetect = GetComponentInChildren<PlayerDetect>();
        _itemDetect = GetComponentInChildren<ItemDetect>();
    }

    private void OnEnable()
    {
        _playerDetect.Undetected += OnUndetected;
        _itemDetect.Undetected += OnUndetected;
    }

    private void OnDisable()
    {
        _playerDetect.Undetected -= OnUndetected;
        _itemDetect.Undetected -= OnUndetected;
    }

    private void OnUndetected()
    {
        OpenTransit();
    }
}

