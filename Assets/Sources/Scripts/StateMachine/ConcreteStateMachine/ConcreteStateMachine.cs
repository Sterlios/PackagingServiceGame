using UnityEngine;

public class ConcreteStateMachine<T> : MonoBehaviour
{
    [SerializeField] private State<T> _startState;

    private State<T> _currentState;

    public void Start()
    {
        ResetMachine();
    }

    private void OnDisable()
    {
        if (_currentState != null)
        {
            _currentState.Exit();
            _currentState = null;
        }
    }

    private void Update()
    {
        if (_currentState == null)
        {
            ResetMachine();
            return;
        }

        State<T> nextState = _currentState.GetNextState();

        if (nextState != null)
            Transit(nextState);
    }

    public void Init(GameObject target)
    {
        _startState.Init(target.GetComponent<T>());
    }

    private void ResetMachine()
    {
        _currentState = _startState;
        _currentState.Enter();
    }

    private void Transit(State<T> nextState)
    {
        if (_currentState != null)
        {
            nextState.Init(_currentState.Target);
            _currentState.Exit();
        }

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter();
    }
}

