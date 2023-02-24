using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(FindFreePointState))]
public class MoveToPointTransition : Transition<WayPoint>
{
    private FindFreePointState _findFreePointState;

    private void Awake()
    {
        _findFreePointState = GetComponent<FindFreePointState>();
    }

    private void OnEnable()
    {
        _findFreePointState.FoundPoint += OnFoundPoint;
    }

    private void OnDisable()
    {
        _findFreePointState.FoundPoint -= OnFoundPoint;
    }

    private void OnFoundPoint()
    {
        OpenTransit();
    }
}
