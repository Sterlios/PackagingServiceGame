using System;
using UnityEngine;

[Serializable]
public class Range
{
    [SerializeField] private int _min;
    [SerializeField] private int _max;

    private static System.Random _random = new();

    public void SetRange(int min, int max)
    {
        _min = min;
        _max = max;
    }

    public int Value => _random.Next(_min, _max);
}

