using UnityEngine;

[RequireComponent(typeof(ActionAnimator))]
class Interaptable : MonoBehaviour
{
    private ActionAnimator _animator;

    private void Awake()
    {
        _animator = GetComponent<ActionAnimator>();
    }

    public void Interapt()
    {
        _animator.SetAnimatorParameter(ActionAnimator.InteraptParameterHash);
    }
}
