using UnityEngine;

[RequireComponent(typeof(MoveToPointState))]
public class IdleTransition : Transition<WayPoint>
{
    private MoveToPointState _moveToPointState;

    private void Awake()
    {    
        _moveToPointState = GetComponent<MoveToPointState>();
    }

    private void OnEnable()
    {
        _moveToPointState.Moved += OnMoved;
    }

    private void OnDisable()
    {
        _moveToPointState.Moved -= OnMoved;
    }

    private void OnMoved()
    {
        OpenTransit();
    }
}