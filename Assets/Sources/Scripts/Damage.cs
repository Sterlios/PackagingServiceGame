using System;
using UnityEngine;

[Serializable]
class Damage
{
    [SerializeField] private int _min;
    [SerializeField] private int _max;

    private System.Random _random;

    public Damage()
    {
        _random = new System.Random();
    }

    public float Value => _random.Next(_min, _max);
}

