using UnityEngine;

public class Storage : MonoBehaviour
{
    public bool HasItem => CurrentItem != null;
    public Item CurrentItem { get; private set; }

    public void Put(Item item)
    {
        if (!CurrentItem)
            CurrentItem = item;
    }

    public Item Drop()
    {
        Item item = CurrentItem;
        CurrentItem = null;
        return item;
    }
}
