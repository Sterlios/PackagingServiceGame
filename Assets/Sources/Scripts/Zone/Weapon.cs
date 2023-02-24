using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Range _damage;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Health target))
            target.TakeDamage(_damage.Value);
    }
}
