using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : Singleton<CoinManager>
{
    [SerializeField] private PlayerHealth playerHealth;

    public static Action<int> OnUpdateCounter;

    public void TakeCoin(int coinsToTake)
    {
        if (!IsCanTakeCoins(coinsToTake))
        {
            Debug.LogWarning("Not Enough Coins!");
            return;
        }

        playerHealth.Paymant(coinsToTake);
    }
    public bool IsCanTakeCoins(int coinsToTake)
    {
        return playerHealth.CurrentHealth - coinsToTake < 0 ? false : true;
    }

    public void AddCoin()
    {
        playerHealth.CurrentHealth++;
    }
}
