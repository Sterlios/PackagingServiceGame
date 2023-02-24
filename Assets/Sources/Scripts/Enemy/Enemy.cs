using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public Health Health { get; private set; }

    public event UnityAction<Enemy> Died;

    private void Awake()
    {
        Health = GetComponentInChildren<Health>();
    }

    private void OnEnable()
    {
        Health.gameObject.SetActive(true);
        Health.Died += OnDied;
    }

    private void OnDisable()
    {
        Health.gameObject.SetActive(false);
        Health.Died -= OnDied;
    }

    private void OnDied()
    {
        Died?.Invoke(this);
    }
}
