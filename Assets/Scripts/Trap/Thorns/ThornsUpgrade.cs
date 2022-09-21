using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornsUpgrade : BaseUpgrade, IPaymant
{
    private static PrefsValue<int> currentUpgrade;

    public static Action<UpgradeVariant, float> UpgradeEvent;

    protected void Start ()
    {
        currentUpgrade = new PrefsValue<int>(prefsName, 0);

        Init(ref currentUpgrade);
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerPaymant player) && CanPaymant())
        {
            paymantUI.gameObject.SetActive(true);
        }
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


    public void Paymant()
    {
        BaseInteractive(ref currentUpgrade, () => Upgrade());
    }

    public bool CanPaymant()
    {
        return CanUpgrade(currentUpgrade.Value);
    }
}
