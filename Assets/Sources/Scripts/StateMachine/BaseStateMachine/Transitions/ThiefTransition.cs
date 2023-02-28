
using UnityEngine;

class ThiefTransition : BaseTransition
{
    private ItemDetect _itemDetect;

    private void Start()
    {
        _itemDetect = GetComponentInChildren<ItemDetect>();
        _itemDetect.Detected += OnDetected;
    }

    private void OnDisable()
    {
        _itemDetect.Detected -= OnDetected;
    }

    private void OnDetected(Item item)
    {
        TargetState.Init(item.gameObject);

        OpenTransit();
    }
}

