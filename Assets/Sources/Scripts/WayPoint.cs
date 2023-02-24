using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public static int IDs = 0;
    public int Id { get; private set; }

    private void Awake()
    {
        IDs++;
        Id = IDs;
        gameObject.name = $"WayPoint {Id}";
    }
}
