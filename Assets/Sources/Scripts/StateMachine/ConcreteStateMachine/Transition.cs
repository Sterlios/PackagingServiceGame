using UnityEngine;

public abstract class Transition<T> : MonoBehaviour
{
    [SerializeField] private State<T> _targetState;

    public bool NeedTransit { get; private set; }

    public State<T> TargetState => _targetState;

    public void OpenTransit()
    {
        NeedTransit = true;
    }

    public void CloseTransit()
    {
        NeedTransit = false;
    }
}


