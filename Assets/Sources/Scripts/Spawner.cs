using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Spawner<T> : MonoBehaviour
{
    [SerializeField] private List<T> _types;
    [SerializeField] private int _count;
    [SerializeField] private Range _timeRange;

    private readonly System.Random _random = new System.Random();
    private List<T> _unactiveObjects;
    private List<T> _activeObjects;

    private float _spawnTime;
    private float _timer;

    public event UnityAction<T> ActivatedObject;
    public event UnityAction<T> DeactivatedObject;

    protected T ActivatingObject { get; private set; }
    protected T Template => _types[_random.Next(_types.Count)];

    private void Awake()
    {
        _unactiveObjects = new List<T>(_count);
        _activeObjects = new List<T>(_count);

        _spawnTime = _timeRange.Value;

        for (int i = 0; i < _count; i++)
            Create();
    }

    private void Update()
    {
        if (_unactiveObjects.Count > 0)
        {
            _timer += Time.deltaTime;

            if (_timer >= _spawnTime)
            {
                _timer = 0;
                _spawnTime = _timeRange.Value;

                Activate();
            }
        }
    }

    public virtual void Activate()
    {
        ActivatingObject = _unactiveObjects[0];
        _unactiveObjects.RemoveAt(0);
        _activeObjects.Add(ActivatingObject);
        ActivatedObject?.Invoke(ActivatingObject);
    }

    public virtual void Deactivate(T t)
    {
        if(_activeObjects.Contains(t))
            _activeObjects.Remove(t);

        _unactiveObjects.Add(t);
        DeactivatedObject?.Invoke(t);
    }

    public abstract void Create();
}
