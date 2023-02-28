using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FindFreePointState : State<WayPoint>
{
    private WalkingWay _walkingWay;

    private WaitForSeconds _wait = new WaitForSeconds(0.5f);
    private Coroutine _lookingForJob;
    private static string _idleAnimationName = "Idle";
    private int _idleAnimationHash = Animator.StringToHash(_idleAnimationName);
    private Animator _animator;

    public event UnityAction FoundPoint;

    private void Start()
    {
        _walkingWay = GetComponentInParent<EnemySpawner>().PatrolWay;
        _animator = GetComponentInParent<Animator>();
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
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName(_idleAnimationName))
            _animator.Play(_idleAnimationHash);

        bool isCorrectPoints = false;

        while (!isCorrectPoints)
        {
            if (_walkingWay.TryGetWayPoint(out WayPoint nextWayPoint))
            {
                _walkingWay.SetFreePoint(Target);
                Init(nextWayPoint);
                FoundPoint?.Invoke();
                isCorrectPoints = true;
            }

            yield return _wait;
        }
    }
}

