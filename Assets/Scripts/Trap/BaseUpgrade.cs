using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUpgrade : MonoBehaviour
{
    [SerializeField] protected PaymantUI paymantUI;
    [SerializeField] protected UpgradeSetting[] upgradeSequence;

    public virtual void UpgradeDamage ()
    {
    }
    public virtual void UpgradeRange()
    {
    }
    public virtual void UpgradeRateOfFire()
    {
    }
    public virtual void UpgradeKillsForOne()
    {
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
    RANGE,
    RATE_OF_FIRE,
    KILLS_FOR_ONE
}
