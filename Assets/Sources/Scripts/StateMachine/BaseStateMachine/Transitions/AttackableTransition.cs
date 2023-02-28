
class AttackableTransition : BaseTransition
{
    private PlayerDetect _playerDetect;

    private void Start()
    {
        _playerDetect = GetComponentInChildren<PlayerDetect>();
    }

    private void OnEnable()
    {
        _playerDetect.Detected += OnDetected;
    }

    private void OnDisable()
    {
        _playerDetect.Detected -= OnDetected;
    }

    private void OnDetected(Player player)
    {
        TargetState.Init(player);

        OpenTransit();
    }
}
