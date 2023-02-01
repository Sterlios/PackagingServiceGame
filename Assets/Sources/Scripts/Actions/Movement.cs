using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
[RequireComponent(typeof(ActionAnimator))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _moveSpeedMultiply;
    [SerializeField] private float _rotateSpeed;

    private ActionAnimator _animator;

    public ActionAnimator Animations => _animator;

    private void Awake()
    {
        _animator = GetComponent<ActionAnimator>();
    }

    public void Move(Vector3 direction, bool isRun = false)
    {
        bool isMove = direction != Vector3.zero;
        float speed = GetSpeed(isMove, isRun);

        if (isMove)
        {
            Rotate(direction);

            Vector3 newPosition = transform.position + direction;
            transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        }

        _animator.SetAnimatorParameter(ActionAnimator.SpeedParameterHash, speed);
    }

    private float GetSpeed(bool isMove, bool isRun)
    {
        float speed = 0;

        if (isMove)
        {
            speed = _moveSpeed;

            if (isRun)
                speed *= _moveSpeedMultiply;
        }

        return speed;
    }

    private void Rotate(Vector3 direction)
    {
        if (Vector3.Angle(transform.forward, direction) > 0)
        {
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, _rotateSpeed * Time.deltaTime, 0);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
}
