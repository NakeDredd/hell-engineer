using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : Singleton<CoinManager>
{
    [SerializeField] private int coins;

    public static Action<int> OnUpdateCounter;

    private void Start()
    {
        Coins = coins;
    }

    public int Coins { 
        get => coins; 
        private set
        {
            coins = value;
            OnUpdateCounter?.Invoke(coins);
        }
    }

    public void TakeCoin(int coinsToTake)
    {
        if (!IsCanTakeCoins(coinsToTake))
        {
            Debug.LogWarning("Not Enough Coins!");
            return;
        }
        Coins -= coinsToTake;
    }
    public bool IsCanTakeCoins(int coinsToTake)
    {
        return Coins - coinsToTake < 0 ? false : true;
    }

    public void AddCoin()
    {
        Coins++;
    }
}
