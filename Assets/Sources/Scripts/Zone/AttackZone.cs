using UnityEngine;

public class AttackZone : MonoBehaviour
{
    [SerializeField] private Damage _damage;

    private Health _target;

    private void OnTriggerEnter(Collider other)
    {
        _target = other.GetComponent<Health>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Health>(out _))
            _target = null;
    }

    public void Attack()
    {
        _target?.TakeDamage(_damage.Value);
    }
}
