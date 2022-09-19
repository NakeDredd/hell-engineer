using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornSaver : MonoBehaviour
{
    [SerializeField] private string thornsPathName;
    [SerializeField] private TrapState trapState;
    [SerializeField] private ThornsTrap trap;

    private void Start()
    {
        ThornsUpgrade.UpgradeEvent += Upgrade;

        ThornsCharacterSerializable thorns = new ThornsCharacterSerializable();

        thorns = ThornsCharacterSerializable.GetCharacter(thornsPathName);

        if (thorns == default)
        {
            return;
        }

        trap.UpgradeDamage(thorns.damage);
        trapState.SetReloadTime(thorns.reloadTime);
        trapState.SetKillsForOne(thorns.killsForOne);
    }
    private void Upgrade(UpgradeVariant up, float value)
    {
        switch (up)
        {
            case UpgradeVariant.DAMAGE:
                {
                    trap.UpgradeDamage((int)value);
                }
                break;
            case UpgradeVariant.RATE_OF_FIRE:
                {
                    trapState.UpgradeReloadTime(value);
                }
                break;
            case UpgradeVariant.KILLS_FOR_ONE:
                {
                    trapState.UpgradeKillsForOne((int)value);
                }
                break;
        }

        Save();
    }
    private void Save()
    {
        ThornsCharacterSerializable thorns = new ThornsCharacterSerializable();

        thorns.SaveThornsCharacters(thornsPathName, trapState.ReloadTime, trapState.KillsForOne, trap.Damage);
    }
}

[Serializable]
public class ThornsCharacterSerializable
{
    public float reloadTime;
    public int killsForOne;
    public int damage;

    public void SaveThornsCharacters(string path, float reloadTime, int killsForOne, int damage)
    {
        this.reloadTime = reloadTime;
        this.killsForOne = killsForOne;
        this.damage = damage;

        SaveClass.Save<ThornsCharacterSerializable>(path, this);
    }

    public static ThornsCharacterSerializable GetCharacter(string path)
    {
        return SaveClass.GetSave<ThornsCharacterSerializable>(path);
    }
}
