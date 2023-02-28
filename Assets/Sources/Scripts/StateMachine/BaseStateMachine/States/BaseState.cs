using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    protected GameObject Target { get; private set; }

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

    public virtual void Init(GameObject gameObject)
    {
        Target = gameObject;
    }
}