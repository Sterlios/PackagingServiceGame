using UnityEngine;

public class EnemySpawner : Spawner<Enemy>
{
    [SerializeField] private WalkingWay _walkingWay;

    public WalkingWay PatrolWay => _walkingWay;

    public override void Create()
    {
        Enemy enemy = Instantiate(Template, transform);
        Deactivate(enemy);
    }

    public override void Activate()
    {
        base.Activate();

        ActivatingObject.gameObject.SetActive(true);
        ActivatingObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
        ActivatingObject.Died += OnEnemyDied;
    }
    
    public override void Deactivate(Enemy enemy)
    {
        base.Deactivate(enemy);
        enemy.gameObject.SetActive(false);
    }

    private void OnEnemyDied(Enemy enemy)
    {
        Deactivate(enemy);
    }
}

