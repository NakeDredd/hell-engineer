using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornsUpgrade : BaseUpgrade, IInteractive
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
        if (collision.TryGetComponent(out PlayerInteraction player) && player.GetInteractive(this) && CanInteractive())
        {
            paymantUI.gameObject.SetActive(true);
        }
    }
    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerInteraction player))
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
        return CanUpgrade(currentUpgrade.Value);
    }

    public void Interactive()
    {
        BaseInteractive(ref currentUpgrade, () => Upgrade());
    }
    public void StopInteractive()
    {

    }
}
