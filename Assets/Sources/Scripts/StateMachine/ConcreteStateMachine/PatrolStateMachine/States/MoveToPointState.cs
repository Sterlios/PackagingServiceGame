using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MoveToPointTransition))]
public class MoveToPointState : State<WayPoint>
{
    [SerializeField] private float _speed;
    private Coroutine _moveJob;
    private Enemy _enemy;

    public event UnityAction Moved;

    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
    }

    private void Update()
    {
        if (_moveJob == null)
            _moveJob = StartCoroutine(Move());
    }

    private void OnDisable()
    {
        if (_moveJob != null)
            StopCoroutine(_moveJob);

        _moveJob = null;
    }

    private IEnumerator Move()
    {
        while (Target.transform.position != _enemy.transform.position)
        {
            _enemy.transform.position = Vector3.MoveTowards(_enemy.transform.position, Target.transform.position, _speed * Time.deltaTime);

            yield return null;
        }

        Moved?.Invoke();
    }
}
