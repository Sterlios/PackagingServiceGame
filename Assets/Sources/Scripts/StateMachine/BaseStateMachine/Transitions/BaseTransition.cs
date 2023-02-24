using UnityEngine;

class BaseTransition : MonoBehaviour
{
    [SerializeField] private BaseState _targetState;

    public bool NeedTransit { get; private set; }

    public BaseState TargetState => _targetState;

    private void OnEnable()
    {
        NeedTransit = false;
    }

    public void OpenTransit()
    {
        NeedTransit = true;
    }
}