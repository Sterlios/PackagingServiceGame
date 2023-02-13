using System.Collections.Generic;
using TMPro;
using UnityEngine;

class CreatingDamageText : MonoBehaviour
{
    [SerializeField] private List<Health> _healthes;
    [SerializeField] private Camera _camera;
    [SerializeField] private FlyingText _templateDamageText;

    private int _queueSize;
    private Queue<FlyingText> _texts;

    private FlyingText _damage;

    private void Awake()
    {
        _queueSize = 10;
        _texts = new Queue<FlyingText>(_queueSize);

        for (int i = 0; i < _queueSize; i++)
        {
            FlyingText newTextUI = Instantiate(_templateDamageText, transform);
            AddTextUI(newTextUI);
        } 
    }

    private void OnEnable()
    {
        foreach(Health health in _healthes)
            health.DecreasedHealth += OnDecreasedHealth;
    }

    private void OnDisable()
    {
        foreach (Health health in _healthes)
            health.DecreasedHealth -= OnDecreasedHealth;
    }

    private void OnDecreasedHealth(Vector3 position, float damage)
    {
        Vector3 newPosition = _camera.WorldToScreenPoint(position);
        string text = damage.ToString();
        FlyingText nextText = _texts.Dequeue();
        nextText.gameObject.SetActive(true);
        nextText.FinishedFly += OnFinishedText;
        nextText.Move(newPosition, text);
    }

    private void OnFinishedText(FlyingText textUI)
    {
        AddTextUI(textUI);
    }

    private void AddTextUI(FlyingText textUI)
    {
        textUI.gameObject.SetActive(false);
        _texts.Enqueue(textUI);
    }
}
