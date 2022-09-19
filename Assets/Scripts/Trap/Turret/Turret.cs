using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Turret : BaseTrap
{
    [SerializeField] private TurretCharacters turretCharacters;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private LayerMask enemyMask;

    private List<EnemyAIBehavior> enemies = new List<EnemyAIBehavior>();

    private void FixedUpdate()
    {
        turretCharacters.CurrentReloadTime -= Time.fixedDeltaTime;

        if (enemies.Count == 0 || turretCharacters.CurrentReloadTime > 0)
        {
            return;
        }

        Ray2D ray = new Ray2D(transform.position, enemies[0].transform.position - transform.position);

        if (Physics2D.Raycast(ray.origin, ray.direction, 100, enemyMask).collider.TryGetComponent(out EnemyAIBehavior enemy))
        {
            if (enemy.transform.position.x > transform.position.x)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }

            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, enemy.transform.position);

            Observable.Timer(System.TimeSpan.FromSeconds(turretCharacters.RenderTime)).TakeUntilDisable(gameObject).Subscribe(_ => lineRenderer.enabled = false);

            enemy.GetDamage(turretCharacters.Damage);

            turretCharacters.UpdateCurretnReloadTime();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyAIBehavior enemy))
        {
            enemies.Add(enemy);

            enemies.OrderBy(x => (transform.position - x.transform.position).magnitude);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyAIBehavior enemy))
        {
            enemies.Remove(enemy);

            enemies.OrderBy(x => (transform.position - x.transform.position).magnitude);
        }
    }

    
}
