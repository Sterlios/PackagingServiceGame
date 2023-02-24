using UnityEngine;

[RequireComponent(typeof(IdleState))]
public class FindFreePointTransition : Transition<WayPoint>
{
    private IdleState _idleState;

    private void Awake()
    {
        _idleState = GetComponent<IdleState>();
    }

    private void OnEnable()
    {
        _idleState.FinishedTimer += OnFinishedTimer;
    }

    private void OnDisable()
    {
        _idleState.FinishedTimer -= OnFinishedTimer;
    }

    private void OnFinishedTimer()
    {
        OpenTransit();
    }
}
