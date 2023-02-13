using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    
    private float _currentHealth;

    public event UnityAction<Vector3, float> DecreasedHealth;
    public event UnityAction<float> ChangedHealth;
    public event UnityAction Died;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        float takenDamage = damage > 0 ? damage : 0;
        _currentHealth = Mathf.Clamp(_currentHealth - takenDamage, 0, _maxHealth);
        DecreasedHealth?.Invoke(transform.position, damage);
        ChangedHealth?.Invoke(_currentHealth);

        if (_currentHealth == 0)
            Die();
    }

    private void Die()
    {
        Died?.Invoke();
    }
}

