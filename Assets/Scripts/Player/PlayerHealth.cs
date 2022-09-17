using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;

    private int currentHealth;

    public delegate void PlayerHealthDelegate(int health);
    public static PlayerHealthDelegate OnPlayerHpChange;

    public int CurrentHealth {
        get => currentHealth;
        set 
        { 
            currentHealth = value;
            OnPlayerHpChange?.Invoke(currentHealth);
        }
    }

    public void GetDamage(int damage)
    {
        if (CurrentHealth - damage <= 0)
        {
            Death();
        }
        else
        {
            CurrentHealth -= damage;
        }
    }

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    private void Death()
    {
        CurrentHealth = 0;
        EndGameController.Instance.LoseTheGame();
        Destroy(gameObject);
    }
}
