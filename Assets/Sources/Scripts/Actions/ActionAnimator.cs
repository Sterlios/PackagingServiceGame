using UnityEngine;

public class ActionAnimator : MonoBehaviour
{
    private Animator _animator;

    public static string AttackAnimationName => "Attack";
    public static string IdleAnimationName => "Idle";
    public static string AttackIdleAnimationName => "AttackIdle";
    public static int CarryingParameterHash => Animator.StringToHash("isCarrying");
    public static int SpeedParameterHash => Animator.StringToHash("Speed");
    public static int AttackParameterHash => Animator.StringToHash("isAttack");
    public static int PackingParameterHash => Animator.StringToHash("isPacking");
    public static int TakeDamageParameterHash => Animator.StringToHash("isTakeDamage");
    public static int DeadParameterHash => Animator.StringToHash("isDead");
    public static int BreakActionParameterHash => Animator.StringToHash("BreakAction");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public bool IsAnimationPlay(string name) => _animator.GetCurrentAnimatorStateInfo(0).IsName(name);

    public void SetAnimatorParameter(int hash)
    {
        _animator.SetTrigger(hash);
    }

    public void SetAnimatorParameter(int hash, bool value)
    {
        _animator.SetBool(hash, value);
    }

    public void SetAnimatorParameter(int hash, float value)
    {
        _animator.SetFloat(hash, value);
    }

}