using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Detect))]
public class PlayerDetect : MonoBehaviour
{
    private Detect _detect;

    public event UnityAction<Player> Detected;
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
        if (other.TryGetComponent(out Player _))
            _detect.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player _))
            _detect.Remove(other.gameObject);
    }

    private void OnDetected(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out Player player))
            Detected?.Invoke(player);
    }

    private void OnUndetected(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out Player _))
            Undetected?.Invoke();
    }
}
