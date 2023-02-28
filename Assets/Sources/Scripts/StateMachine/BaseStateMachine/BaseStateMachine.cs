using UnityEngine;

public class BaseStateMachine : MonoBehaviour
{
    [SerializeField] private BaseState _startState;
    
    private BaseState _currentState;

    public void Awake()
    {
        ResetMachine();
    }

    private void Update()
    {
        if (_currentState == null)
            ResetMachine();

        BaseState nextState = _currentState.NextState;

        if (nextState != null)
            Transit(nextState);
    }

    private void ResetMachine()
    {
        _currentState = _startState;
        _currentState.Enter();
    }

    private void Transit(BaseState nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter();
    }
}

