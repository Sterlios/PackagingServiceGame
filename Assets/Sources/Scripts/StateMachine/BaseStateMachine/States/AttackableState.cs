class AttackableState : BaseState
{
    private AttackStateMachine _attackStateMachine;

    private void Start()
    {
        _attackStateMachine = GetComponentInChildren<AttackStateMachine>();
        _attackStateMachine.enabled = false;
        base.Exit();
    }

    public override void Enter()
    {
        _attackStateMachine.enabled = true;
        _attackStateMachine.Init(Target);

        base.Enter();
    }

    public override void Exit()
    {
        _attackStateMachine.enabled = false;

        base.Exit();
    }
}