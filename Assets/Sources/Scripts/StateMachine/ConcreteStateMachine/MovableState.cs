
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class MovableState<T> : State<T>
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;

    private Coroutine _moveJob;
    private Coroutine _rotateJob;
    private Enemy _enemy;
    private string _walkAnimationName = "Walk";
    private int _walkAnimationHash = Animator.StringToHash("Walk");
    private Animator _animator;

    protected Enemy ThisEnemy => _enemy;
    protected float MoveSpeed => _moveSpeed;
    protected float RotateSpeed => _rotateSpeed;

    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
        _animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (_moveJob == null)
        {
            if (!_animator.GetCurrentAnimatorStateInfo(0).IsName(_walkAnimationName))
                _animator.Play(_walkAnimationHash);

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

        _moveJob = null;
        _rotateJob = null;
    }

    public abstract IEnumerator Move();

    public abstract IEnumerator Rotate();
}
