using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgrade : BaseUpgrade, IInteractive
{
    private static PrefsValue<int> currentUpgrade;

    public static Action<UpgradeVariant, float> UpgradeEvent;

    protected void Start()
    {
        currentUpgrade = new PrefsValue<int>(prefsName, 0);

        Init(currentUpgrade.Value);
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerPaymant player))
        {
            paymantUI.gameObject.SetActive(false);
        }
    }

    private void Upgrade()
    {
        UpgradeEvent?.Invoke(upgradeSequence[currentUpgrade.Value].upgradeVarinat, upgradeSequence[currentUpgrade.Value].value);
    }

    public bool CanInteractive()
    {
        if (CanUpgrade(currentUpgrade.Value) && !TrapGenerator.Instance.PickUp)
        {
            paymantUI.gameObject.SetActive(true);
            return true;
        }

        return false;
    }

    public void Interactive()
    {
        BaseInteractive(ref currentUpgrade, () => Upgrade());
    }

    public void StopInteractive()
    {

    }
}
