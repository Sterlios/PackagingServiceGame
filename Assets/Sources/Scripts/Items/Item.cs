using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private float _packingTime;
    [SerializeField] private float _step;

    public float CurrentProgress { get; private set; } = 0;
    public bool IsPacking => CurrentProgress == _packingTime;

    public IEnumerator Pack()
    {
        while (CurrentProgress != _packingTime)
        {
            CurrentProgress = Mathf.MoveTowards(CurrentProgress, _packingTime, _step * Time.deltaTime);
            yield return null;
        }
    }
}
