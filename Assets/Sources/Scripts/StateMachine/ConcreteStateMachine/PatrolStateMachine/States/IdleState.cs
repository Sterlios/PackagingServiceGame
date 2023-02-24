using UnityEngine;
using UnityEngine.Events;

public class IdleState : State<WayPoint>
{
    [SerializeField] private Range _timerRange;

    private float _maxTime;
    private float _timer;

    public event UnityAction FinishedTimer;

    private void OnEnable()
    {
        _maxTime = _timerRange.Value;
        _timer = 0;
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
