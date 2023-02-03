using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Attackable))]
[RequireComponent(typeof(Carrying))]
[RequireComponent(typeof(Interaptable))]
public class Player : MonoBehaviour, IAttackable, IMovable
{
    private Movement _movement;
    private Attackable _attack;
    private Interactable _interactable;
    private Interaptable _interaptable;

    private PlayerInput _input;

    public event UnityAction Interapted;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Enable();

        _movement = GetComponent<Movement>();
        _attack = GetComponent<Attackable>();
        _interactable = GetComponent<Carrying>();
        _interaptable = GetComponent<Interaptable>();
    }

    private void Update()
    {
        Move();
        Attack();
        Interact();
    }

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent(out _interactable);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Interactable>(out _))
            _interactable = GetComponent<Carrying>();
    }

    public void Move()
    {
        Vector2 direction2D = _input.Player.Move.ReadValue<Vector2>();
        Vector3 direction3D = new Vector3(direction2D.x, 0, direction2D.y);
        
        if(direction3D != Vector3.zero)
            Interapted?.Invoke();

        bool isRun = _input.Player.IncreaseSpeed.IsPressed();
        _movement.Move(direction3D, isRun);
    }

    public void Attack()
    {
        bool isAttack = _input.Player.Attack.triggered;

        if (isAttack)
        {
            Interapted?.Invoke();
            _attack.Attack();
        }
    }

    public void Interapt()
    {
        _interaptable.Interapt();
    }

    private void Interact()
    {
        bool isInteract = _input.Player.Interactive.triggered;

        if (isInteract)
            _interactable.Interact();
    }
}
