class ThiefState : BaseState
{
    private ThiefStateMachine _thiefStateMachine;

    private void Start()
    {
        _thiefStateMachine = GetComponentInChildren<ThiefStateMachine>();
        _thiefStateMachine.enabled = false;
        base.Exit();
    }

    public override void Enter()
    {
        _thiefStateMachine.enabled = true;
        _thiefStateMachine.Init(Target);

        base.Enter();
    }

    public override void Exit()
    {
        _thiefStateMachine.enabled = false;

        base.Exit();
    }
}