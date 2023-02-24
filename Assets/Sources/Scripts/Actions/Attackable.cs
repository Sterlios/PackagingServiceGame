using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ActionAnimator))]
public class Attackable : MonoBehaviour
{
    private ActionAnimator _animator;
    private Weapon _weapon;
    
    private bool IsAttackAnimation => _animator.IsAnimationPlay(ActionAnimator.AttackAnimationName);
    private bool IsIdleAnimation => _animator.IsAnimationPlay(ActionAnimator.IdleAnimationName);
    private bool IsAttackIdleAnimation => _animator.IsAnimationPlay(ActionAnimator.AttackIdleAnimationName);
    public ActionAnimator Animations => _animator;

    private void Awake()
    {
        _animator = GetComponent<ActionAnimator>();
        _weapon = GetComponentInChildren<Weapon>();
    }

    public void Attack()
    {
        if (!IsAttackAnimation)
            if(IsIdleAnimation || IsAttackIdleAnimation)
                _animator.SetAnimatorParameter(ActionAnimator.AttackParameterHash);
    }
}
