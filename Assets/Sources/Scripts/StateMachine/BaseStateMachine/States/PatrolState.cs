public class PatrolState : BaseState
{
    private PatrolStateMachine _patrolStateMachine;

    private void Start()
    {
        _patrolStateMachine = GetComponentInChildren<PatrolStateMachine>();
        _patrolStateMachine.enabled = false;
    }

    public override void Enter()
    {
        _patrolStateMachine.enabled = true;

        base.Enter();
    }

    public override void Exit()
    {
        _patrolStateMachine.enabled = false;

        base.Exit();
    }
}