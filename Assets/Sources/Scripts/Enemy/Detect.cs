using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Detect : MonoBehaviour
{
    public event UnityAction<GameObject> Detected;
    public event UnityAction<GameObject> Undetected;

    private GameObject _detectedObject;

    private List<GameObject> _detectedObjects = new List<GameObject>();

    private void Update()
    {
        if (_detectedObject == null)
        {
            if (_detectedObjects.Count > 0)
            {
                _detectedObject = _detectedObjects[0];
                Detected?.Invoke(_detectedObject);
            }
        }
    }

    public void Add(GameObject gameObject)
    {
        if (!_detectedObjects.Contains(gameObject))
        {
            _detectedObjects.Add(gameObject);
        }
    }

    public void Remove(GameObject gameObject)
    {
        if (_detectedObjects.Contains(gameObject))
        {
            _detectedObjects.Remove(gameObject);

            if (_detectedObject == gameObject)
            {
                _detectedObject = null;
                Undetected?.Invoke(gameObject);
            }
        }
    }
}