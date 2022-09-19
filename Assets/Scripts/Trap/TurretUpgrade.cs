using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgrade : BaseUpgrade, IInteractive
{
    private static PrefsValue<int> currentUpgrade;

    public static Action<UpgradeVariant, float> UpgradeEvent;

    private void Start()
    {
        currentUpgrade = new PrefsValue<int>("CurrentTurretUpgrade", 0);

        if (upgradeSequence.Length <= currentUpgrade.Value)
        {
            return;
        }

        paymantUI.Init(upgradeSequence[currentUpgrade.Value].price);
    }
    private void Upgrade()
    {
        UpgradeEvent?.Invoke(upgradeSequence[currentUpgrade.Value].upgradeVarinat, upgradeSequence[currentUpgrade.Value].value);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerInteraction player) && player.GetInteractive(this) && CanInteractive())
        {
            paymantUI.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerInteraction player))
        {
            paymantUI.gameObject.SetActive(false);
        }
    }

    public bool CanInteractive()
    {
        if (upgradeSequence.Length <= currentUpgrade.Value)
        {
            return false;
        }

        return true;
    }

    public void Interactive()
    {
        if (!CoinManager.Instance.IsCanTakeCoins(1))
        {
            return;
        }

        CoinManager.Instance.TakeCoin(1);

        if (paymantUI.ChangeNextCoin())
        {
            paymantUI.ClearSprites();
            Upgrade();
            currentUpgrade.Value++;

            if (CanInteractive())
            {
                paymantUI.Init(upgradeSequence[currentUpgrade.Value].price);
            }
            else
            {
                paymantUI.gameObject.SetActive(false);
            }
        }
    }
    public void StopInteractive()
    {
    }
}
