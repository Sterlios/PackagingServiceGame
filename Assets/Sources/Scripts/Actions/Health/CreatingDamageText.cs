using System.Collections.Generic;
using TMPro;
using UnityEngine;

class CreatingDamageText : MonoBehaviour
{
    [SerializeField] private List<EnemySpawner> _spawners;
    [SerializeField] private List<Health> _healthes;
    [SerializeField] private Camera _camera;
    [SerializeField] private FlyingText _templateDamageText;

    private int _queueSize;
    private Queue<FlyingText> _unactiveDamageTexts;
    private Queue<FlyingText> _activeDamageTexts;

    private FlyingText _damage;

    private void Awake()
    {
        _queueSize = 10;
        _unactiveDamageTexts = new Queue<FlyingText>(_queueSize);
        _activeDamageTexts = new Queue<FlyingText>(_queueSize);

        for (int i = 0; i < _queueSize; i++)
        {
            FlyingText newTextUI = Instantiate(_templateDamageText, transform);
            AddTextUI(newTextUI);
        } 
    }

    private void OnEnable()
    {
        foreach(var spawner in _spawners)
        {
            spawner.ActivatedObject += OnActivated;
            spawner.DeactivatedObject += OnDeactivated;
        }
    }

    private void OnDisable()
    {
        foreach (var spawner in _spawners)
        {
            spawner.ActivatedObject -= OnActivated;
            spawner.DeactivatedObject -= OnDeactivated;
        }

        foreach (Health health in _healthes)
            health.DecreasedHealth -= OnDecreasedHealth;
    }

    private void OnActivated(Enemy enemy)
    {
        _healthes.Add(enemy.Health);
        enemy.Health.DecreasedHealth += OnDecreasedHealth;
    }

    private void OnDeactivated(Enemy enemy)
    {
        _healthes.Remove(enemy.Health);
        enemy.Health.DecreasedHealth -= OnDecreasedHealth;
    }

    private void OnDecreasedHealth(Vector3 position, float damage)
    {
        string text = damage.ToString();
        
        FlyingText damageText = _unactiveDamageTexts.Dequeue();
        damageText.gameObject.SetActive(true);
        damageText.FinishedFly += OnFinishedText;
        _activeDamageTexts.Enqueue(damageText);

        Vector3 newPosition = _camera.WorldToScreenPoint(position);
        damageText.Move(newPosition, text);
    }

    private void OnFinishedText()
    {
        FlyingText finishedFlyingText = _activeDamageTexts.Dequeue();          
        finishedFlyingText.FinishedFly -= OnFinishedText;
        AddTextUI(finishedFlyingText);
    }

    private void AddTextUI(FlyingText textUI)
    {
        textUI.gameObject.SetActive(false);
        _unactiveDamageTexts.Enqueue(textUI);
    }
}
