using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TMP_Text))]
public class FlyingText : MonoBehaviour
{
    [SerializeField] private float _distance = 20;
    [SerializeField] private float _step = 20;
    [SerializeField] private float _showTime = 1;
    
    private TMP_Text _damageText;

    public event UnityAction<FlyingText> FinishedFly;

    private Coroutine _moveJob;

    private void Awake()
    {
        _damageText = GetComponent<TMP_Text>();
    }

    public void Move(Vector3 position, string text)
    {
        transform.position = position;
        _damageText.text = text;
        _moveJob = StartCoroutine(MoveText());
    }

    private IEnumerator MoveText()
    {
        float time = 0;
        Vector3 targetPosition = transform.position + Vector3.up * _distance;

        while (time < _showTime)
        {
            time = Mathf.MoveTowards(time, _showTime, Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _step * Time.deltaTime);
            yield return null;
        }

        FinishedFly?.Invoke(this);
        StopCoroutine(_moveJob);
    }
}

