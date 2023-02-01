using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public ActionAnimator Animations { get; private set; }

    public abstract void Interact();

    public void SetAnimator(ActionAnimator actionAnimator)
    {
        Animations = actionAnimator;
    }
}

