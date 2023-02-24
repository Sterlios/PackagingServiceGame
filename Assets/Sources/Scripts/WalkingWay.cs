using System.Collections.Generic;
using UnityEngine;

public class WalkingWay : MonoBehaviour
{
    private List<WayPoint> _freePoints;
    private List<WayPoint> _busyPoints;

    private Range _range;

    private void Start()
    {
        _range = new Range();
        int pointsCount = GetComponentsInChildren<WayPoint>().Length;

        _freePoints = new List<WayPoint>(pointsCount);
        _freePoints.AddRange(GetComponentsInChildren<WayPoint>());

        _busyPoints = new List<WayPoint>(pointsCount);
    }

    public bool TryGetWayPoint(out WayPoint wayPoint)
    {
        wayPoint = null;

        if (_freePoints.Count == 0)
            return false;

        wayPoint = GetWayPoint();

        return true;
    }

    public void SetFreePoint(WayPoint wayPoint)
    {
        ChangeListForWayPoint(_busyPoints, _freePoints, wayPoint);
    }

    private void ChangeListForWayPoint(List<WayPoint> removingList, List<WayPoint> addingList, WayPoint wayPoint)
    {
        removingList.Remove(wayPoint);
        addingList.Add(wayPoint);
    }

    private WayPoint GetWayPoint()
    {
        _range.SetRange(0, _freePoints.Count);
        int nextPointNumber = _range.Value;
        WayPoint wayPoint = _freePoints[nextPointNumber];

        ChangeListForWayPoint(_freePoints, _busyPoints, wayPoint);
        return wayPoint;
    }
}
