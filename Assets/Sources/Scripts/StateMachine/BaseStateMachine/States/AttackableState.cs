class AttackableState : BaseState
{
    private AttackStateMachine _attackStateMachine;

    private void Start()
    {
        _attackStateMachine = GetComponentInChildren<AttackStateMachine>();
        _attackStateMachine.enabled = false;
    }

    public override void Enter()
    {
        _attackStateMachine.enabled = true;

        base.Enter();
    }

    public override void Exit()
    {
        _attackStateMachine.enabled = false;

        base.Exit();
    }

    public override void Init(Player player)
    {
        _attackStateMachine.Init(player);
    }
}