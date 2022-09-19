using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUpgrade : MonoBehaviour
{
    [SerializeField] protected string prefsName;
    [SerializeField] protected PaymantUI paymantUI;
    [SerializeField] protected UpgradeSetting[] upgradeSequence;

    protected virtual void Init(ref PrefsValue<int> currentUpgrade)
    {
        if (upgradeSequence.Length <= currentUpgrade.Value)
        {
            return;
        }

        paymantUI.Init(upgradeSequence[currentUpgrade.Value].price);
    }


    protected void BaseInteractive (ref PrefsValue<int> currentUpgrade, Action Upgrade)
    {
        if (!CoinManager.Instance.IsCanTakeCoins(1))
        {
            return;
        }

        CoinManager.Instance.TakeCoin(1);

        if (paymantUI.ChangeNextCoin())
        {
            paymantUI.ClearSprites();
            Upgrade?.Invoke();
            currentUpgrade.Value++;

            if (CanUpgrade(currentUpgrade.Value))
            {
                paymantUI.Init(upgradeSequence[currentUpgrade.Value].price);
            }
            else
            {
                paymantUI.gameObject.SetActive(false);
            }
        }
    }
    protected bool CanUpgrade(int currentUpgrade)
    {
        if (upgradeSequence.Length <= currentUpgrade)
        {
            return false;
        }

        return true;
    }
}

[Serializable]
public class UpgradeSetting
{
    public UpgradeVariant upgradeVarinat;
    public int price;
    public int value;
}

public enum UpgradeVariant
{
    DAMAGE,
    RANGE_ZONE,
    RATE_OF_FIRE,
    KILLS_FOR_ONE
}
