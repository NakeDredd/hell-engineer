using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : Singleton<CoinManager>
{
    [SerializeField] private int coins;

    public delegate void CoinUpdateDelegate(int coinCount);
    public static CoinUpdateDelegate OnUpdateCounter;

    public int Coins { 
        get => coins; 
        set
        {
            coins = value;
            OnUpdateCounter?.Invoke(coins);
        }
    }

    public void TakeCoin(int coinsToTake)
    {
        if (Coins - coinsToTake <= 0)
        {
            Debug.LogWarning("Not Enough Coins!");
            return;
        }
        Coins--;
    }
    public bool IsCanTakeCoins(int coinsToTake)
    {
        return !(Coins - coinsToTake <= 0);
    }

    public void AddCoin()
    {
        Coins++;
    }
}
