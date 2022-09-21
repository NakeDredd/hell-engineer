using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCharacters : MonoBehaviour
{
    [SerializeField] private string turretPathName;
    [SerializeField] private float reloadTime = 0.1f;
    [SerializeField] private float renderTime = 0.1f;
    [SerializeField] private int damage = 1;
    [SerializeField] private CircleCollider2D collider;

    public float CurrentReloadTime { get; set; }
    public float RenderTime => reloadTime;
    public int Damage => damage;

    private void Start()
    {
        TurretUpgrade.UpgradeEvent += Upgrade;

        TurretCharacterSerializable turret = new TurretCharacterSerializable();

        turret = TurretCharacterSerializable.GetCharacter(turretPathName);

        if (turret == default)
        {
            return;
        }

        reloadTime = turret.reloadTime;
        renderTime = turret.renderTime;
        damage = turret.damage;
    }
    private void OnDestroy()
    {
        TurretUpgrade.UpgradeEvent -= Upgrade;
    }

    private void Upgrade (UpgradeVariant up, float value)
    {
        switch (up)
        {
            case UpgradeVariant.DAMAGE:
                {
                    damage += (int)value;
                }
                break;
            case UpgradeVariant.RANGE_ZONE:
                {
                    collider.radius += value;
                }
                break;
            case UpgradeVariant.RATE_OF_FIRE:
                {
                    reloadTime -= value;
                }
                break;
            case UpgradeVariant.KILLS_FOR_ONE:
                {
                    Debug.Log("мер рср мхусъ");
                }
                break;
        }

        Save();
    }

    private void Save ()
    {
        TurretCharacterSerializable turret = new TurretCharacterSerializable();

        turret.SaveTurretCharacters(turretPathName, reloadTime, renderTime, damage);
    }

    public void UpdateCurretnReloadTime()
    {
        CurrentReloadTime = reloadTime;
    }
}

[Serializable]
public class TurretCharacterSerializable
{
    public float reloadTime;
    public float renderTime;
    public int damage;

    public void SaveTurretCharacters (string path, float reloadTime, float renderTime, int damage)
    {
        this.reloadTime = reloadTime;
        this.renderTime = renderTime;
        this.damage = damage;

        SaveClass.Save<TurretCharacterSerializable>(path, this);
    }

    public static TurretCharacterSerializable GetCharacter (string path)
    {
        return SaveClass.GetSave<TurretCharacterSerializable>(path);
    }
}
