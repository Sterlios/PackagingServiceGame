using UnityEngine;

[RequireComponent(typeof(ActionAnimator))]
class Breakable : MonoBehaviour
{
    private ActionAnimator _animator;

    private void Awake()
    {
        _animator = GetComponent<ActionAnimator>();
    }

    public void BreakAction()
    {
        _animator.SetAnimatorParameter(ActionAnimator.BreakActionParameterHash);
    }
}
