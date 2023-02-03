using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    [SerializeField] private float _packingTime;
    [SerializeField] private float _step;

    public float CurrentProgress { get; private set; } = 0;
    public event UnityAction Packed;
    public bool IsPacking => CurrentProgress == _packingTime;

    public IEnumerator Pack()
    {
        while (!IsPacking)
        {
            CurrentProgress = Mathf.MoveTowards(CurrentProgress, _packingTime, _step * Time.deltaTime);

            if (IsPacking)
                Packed?.Invoke();

            yield return null;
        }
    }
}
