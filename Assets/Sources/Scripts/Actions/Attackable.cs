using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ActionAnimator))]
public class Attackable : MonoBehaviour
{
    private ActionAnimator _animator;

    public ActionAnimator Animations => _animator;

    private void Awake()
    {
        _animator = GetComponent<ActionAnimator>();
    }

    public void Attack()
    {
        bool isAttackAnimation = _animator.IsAnimationPlay(ActionAnimator.AttackAnimationName);
        bool isIdleAnimation = _animator.IsAnimationPlay(ActionAnimator.IdleAnimationName);
        bool isAttackIdleAnimation = _animator.IsAnimationPlay(ActionAnimator.AttackIdleAnimationName);

        if (!isAttackAnimation)
            if(isIdleAnimation || isAttackIdleAnimation)
                _animator.SetAnimatorParameter(ActionAnimator.AttackParameterHash);
    }
}
