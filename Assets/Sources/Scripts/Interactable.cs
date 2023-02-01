using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact();
    public ActionAnimator Animations { get; private set; }

    public void SetAnimator(ActionAnimator actionAnimator)
    {
        Animations = actionAnimator;
    }
}

