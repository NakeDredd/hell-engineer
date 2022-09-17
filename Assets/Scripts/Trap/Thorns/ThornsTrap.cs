using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornsTrap : BaseTrap
{
    [SerializeField] private TrapState trapState;
    [SerializeField] private int damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!trapState.IsActive)
        {
            return;
        }

        if (other.TryGetComponent(out EnemyAIBehavior enemy))
        {
            enemy.GetDamage(damage);

            trapState.SetActive(false);
        }
    }
}
