using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    [SerializeField] private BaseTransition[] _transitions;

    public BaseState NextState
    {
        get
        {
            foreach (BaseTransition transition in _transitions)
                if (transition.NeedTransit)
                    return transition.TargetState;

            return null;
        }
    }

    private void Awake()
    {
        Exit();
    }

    public virtual void Enter()
    {
        if (!enabled)
        {
            enabled = true;

            foreach (BaseTransition transition in _transitions)
                transition.enabled = true;
        }
    }

    public virtual void Exit()
    {
        if (enabled)
        {
            foreach (BaseTransition transition in _transitions)
                transition.enabled = false;

            enabled = false;
        }
    }

    public virtual void Init(Player player)
    {

    }

    public virtual void Init(Item item)
    {

    }
}