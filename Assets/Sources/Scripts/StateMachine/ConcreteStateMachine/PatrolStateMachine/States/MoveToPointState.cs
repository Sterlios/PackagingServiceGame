using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MoveToPointTransition))]
public class MoveToPointState : State<WayPoint>
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;

    private Coroutine _moveJob;
    private Coroutine _rotateJob;
    private Enemy _enemy;
    private string _walkAnimationName = "Walk";
    private int _walkAnimationHash = Animator.StringToHash("Walk");
    private Animator _animator;

    public event UnityAction Moved;
    protected Enemy ThisEnemy => _enemy;
    protected float MoveSpeed => _moveSpeed;
    protected float RotateSpeed => _rotateSpeed;

    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
        _animator = GetComponentInParent<Animator>();
    }

    private void OnEnable()
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName(_walkAnimationName))
            _animator.Play(_walkAnimationHash);
    }

    private void Update()
    {
        if (_moveJob == null)
        {
            _moveJob = StartCoroutine(Move());
            _rotateJob = StartCoroutine(Rotate());
        }
    }

    private void OnDisable()
    {
        if (_moveJob != null)
            StopCoroutine(_moveJob);

        if (_rotateJob != null)
            StopCoroutine(_rotateJob);

        _animator.StopPlayback();
        _moveJob = null;
        _rotateJob = null;
    }

    public IEnumerator Move()
    {
        while (Target.transform.position != ThisEnemy.transform.position)
        {
            ThisEnemy.transform.position = Vector3.MoveTowards(ThisEnemy.transform.position, Target.transform.position, MoveSpeed * Time.deltaTime);

            yield return null;
        }

        Moved?.Invoke();
    }

    public IEnumerator Rotate()
    {
        Vector3 direction = Target.transform.position - ThisEnemy.transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);

        while (Vector3.Angle(transform.forward, direction) > 0)
        {
            ThisEnemy.transform.rotation = Quaternion.Lerp(ThisEnemy.transform.rotation, rotation, RotateSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
