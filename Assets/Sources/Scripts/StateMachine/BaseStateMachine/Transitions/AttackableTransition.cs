
using UnityEngine;

class AttackableTransition : BaseTransition
{
    private PlayerDetect _playerDetect;

    private void Start()
    {
        _playerDetect = GetComponentInChildren<PlayerDetect>();
        _playerDetect.Detected += OnDetected;
    }

    private void OnDisable()
    {
        _playerDetect.Detected -= OnDetected;
    }

    private void OnDetected(Player player)
    {

        TargetState.Init(player.gameObject);
        OpenTransit();
    }
}
