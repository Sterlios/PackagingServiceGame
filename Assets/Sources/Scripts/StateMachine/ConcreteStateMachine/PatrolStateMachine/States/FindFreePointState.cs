using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FindFreePointState : State<WayPoint>
{
    private WalkingWay _walkingWay;

    private WaitForSeconds _wait = new WaitForSeconds(0.5f);
    private Coroutine _lookingForJob;

    public event UnityAction FoundPoint;

    private void Awake()
    {
        _walkingWay = GetComponentInParent<EnemySpawner>().PatrolWay;
    }

    private void OnDisable()
    {
        if (_lookingForJob != null)
            StopCoroutine(_lookingForJob);

        _lookingForJob = null;
    }

    private void Update()
    {
        if (_lookingForJob == null)
            _lookingForJob = StartCoroutine(LookingForFreePoint());
    }

    private IEnumerator LookingForFreePoint()
    {
        WayPoint nextWayPoint = Target;

        bool IsCorrectPoints = false;

        while (IsCorrectPoints != true)
        {
            _walkingWay.TryGetWayPoint(out nextWayPoint);

            if (HasTarget)
                if (nextWayPoint != null)
                    IsCorrectPoints = nextWayPoint.Id != Target.Id;
                else
                    IsCorrectPoints = false;
            else if (nextWayPoint != null)
                IsCorrectPoints = true;
            else
                IsCorrectPoints = false;

            if (IsCorrectPoints == true)
            {
                if (Target != null)
                    _walkingWay.SetFreePoint(Target);

                Init(nextWayPoint);

                FoundPoint?.Invoke();
            }

            yield return _wait;
        }

    }
}

