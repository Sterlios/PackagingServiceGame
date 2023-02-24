using UnityEngine;

public abstract class State<T> : MonoBehaviour
{
    [SerializeField] private Transition<T>[] _transitions;

    public T Target { get; private set; }

    public bool HasTarget => Target != null;
    
    public void Init(T target)
    {
        Target = target;
    }

    public virtual void Enter()
    {
        if (!enabled)
        {
            enabled = true;

            foreach (Transition<T> transition in _transitions)
                transition.enabled = true;
        }
    } 

    public virtual void Exit()
    {
        if (enabled)
        {
            foreach (Transition<T> transition in _transitions)
            {
                transition.CloseTransit();
                transition.enabled = false;
            }

            enabled = false;
        }
    }

    public State<T> GetNextState()
    {
        foreach (Transition<T> transition in _transitions)
            if (transition.NeedTransit)
                return transition.TargetState;

        return null;
    }
}
