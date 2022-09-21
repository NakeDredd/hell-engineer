using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;

    private int currentHealth;

    public static Action<int> OnPlayerHpChange;

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
    public void Paymant(int value)
    {
        if (CurrentHealth - value >= 0)
        {
            CurrentHealth -= value;
        }
    }

    private void Start()
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
