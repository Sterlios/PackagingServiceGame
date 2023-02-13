using UnityEngine;

[RequireComponent(typeof(Health))]
public class HealthAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private Health _health;

    public static int TakeDamageParameterHash => Animator.StringToHash("isTakeDamage");
    public static int DeadParameterHash => Animator.StringToHash("isDead");

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.DecreasedHealth += OnDecreasedHealth;
        _health.Died += OnDied;
    }

    private void OnDecreasedHealth(Vector3 position, float damage)
    {
        _animator?.SetTrigger(TakeDamageParameterHash);
    }

    private void OnDied()
    {
        _animator?.SetTrigger(DeadParameterHash);
    }
}
