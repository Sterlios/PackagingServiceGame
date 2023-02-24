
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    [SerializeField] private BaseTransition[] _transitions;

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
            enabled = false;

            foreach (BaseTransition transition in _transitions)
                transition.enabled = false;
        }
    }

    public BaseState GetNextState()
    {
        foreach (BaseTransition transition in _transitions)
            if (transition.NeedTransit)
                return transition.TargetState;

        return null;
    }
}