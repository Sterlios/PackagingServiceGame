using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    [SerializeField] private float _packingTime;
    [SerializeField] private float _step;

    public float CurrentProgress { get; private set; } = 0;
    public bool IsPacking => CurrentProgress == _packingTime;
    public event UnityAction<Item> Packed;

    public IEnumerator Pack()
    {
        while (!IsPacking)
        {
            CurrentProgress = Mathf.MoveTowards(CurrentProgress, _packingTime, _step * Time.deltaTime);

            if (IsPacking)
                Packed?.Invoke(this);

            yield return null;
        }
    }
}
