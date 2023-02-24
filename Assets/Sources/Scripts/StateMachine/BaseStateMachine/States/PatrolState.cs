using UnityEngine;

public class PatrolState : BaseState
{
    private PatrolStateMachine _patrolStateMachine;

    private void Start()
    {
        _patrolStateMachine = GetComponentInChildren<PatrolStateMachine>();
        _patrolStateMachine.gameObject.SetActive(false);
        enabled = false;
    }

    public override void Enter()
    {
        _patrolStateMachine.gameObject.SetActive(true);

        base.Enter();
    }

    public override void Exit()
    {
        _patrolStateMachine.gameObject.SetActive(false);

        base.Exit();
    }
}