using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Detect))]
public class ItemDetect : MonoBehaviour
{
    private Detect _detect;

    public event UnityAction<Item> Detected;
    public event UnityAction Undetected;

    private void Awake()
    {
        _detect = GetComponent<Detect>();
    }

    private void OnEnable()
    {
        _detect.Detected += OnDetected;
        _detect.Undetected += OnUndetected;
    }

    private void OnDisable()
    {
        _detect.Detected -= OnDetected;
        _detect.Undetected -= OnUndetected;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Item _))
        {
            _detect.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Item _))
        {
            _detect.Remove(other.gameObject);
        }
    }

    private void OnDetected(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out Item item))
        {
            Detected?.Invoke(item);
        }
    }

    private void OnUndetected(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out Item item))
        {
            Undetected?.Invoke();
        }
    }
}