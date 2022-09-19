using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornsTrap : BaseTrap
{
    [SerializeField] private TrapState trapState;
    [SerializeField] private int damage;

    public int Damage => damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!trapState.IsActive)
        {
            return;
        }

        if (other.TryGetComponent(out EnemyAIBehavior enemy))
        {
            enemy.GetDamage(damage);

            trapState.TrySetActive(false);
        }
    }

    public void SetDamage (int value)
    {
        damage = value;
    }
    public void UpgradeDamage(int plusValue)
    {
        damage += plusValue;
        SetDamage(damage);
    }
}
