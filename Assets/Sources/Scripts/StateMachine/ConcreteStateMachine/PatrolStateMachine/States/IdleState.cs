using UnityEngine;
using UnityEngine.Events;

public class IdleState : State<WayPoint>
{
    [SerializeField] private Range _timerRange;

    private float _maxTime;
    private float _timer;
    private string _idleAnimationName = "Idle";
    private int _idleAnimationHash = Animator.StringToHash("Idle");
    private Animator _animator;

    public event UnityAction FinishedTimer;

    private void Awake()
    {
        _animator = GetComponentInParent<Animator>();
    }

    private void OnEnable()
    {
        _maxTime = _timerRange.Value;
        _timer = 0;

        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName(_idleAnimationName))
            _animator.Play(_idleAnimationHash);
    }

    private void Update()
    {
        if (_timer >= _maxTime)
            return;

        _timer += Time.deltaTime;

        if (_timer >= _maxTime)
            FinishedTimer?.Invoke();
    }
}
