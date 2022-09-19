using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TrapState : MonoBehaviour, IInteractive
{
    [SerializeField] private TrapStateUI stateUI;
    [SerializeField] private float reloadTime;
    [SerializeField] private int killsForOne = 1;

    private IDisposable reloadTimer;
    private bool isActive = true;
    private int currentActiveKills;

    public bool IsActive => isActive;
    public float ReloadTime => reloadTime;
    public int KillsForOne => killsForOne;

    private void Start()
    {
        currentActiveKills = killsForOne;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerInteraction player) && player.GetInteractive(this) && !isActive)
        {
            stateUI.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerInteraction player) && !isActive)
        {
            StopReloadTrap();
            stateUI.gameObject.SetActive(false);
        }
    }

    private void ReloadTrap()
    {
        stateUI.ZeroingProgressBar();

        float plus = 100 / reloadTime;

        float currentTime = 0;

        reloadTimer?.Dispose();

        reloadTimer = Observable.EveryFixedUpdate().TakeUntilDisable(gameObject).Subscribe(_ => 
        {
            stateUI.AddProgressBar(plus * Time.fixedDeltaTime);
            currentTime += Time.fixedDeltaTime;

            if (currentTime >= reloadTime)
            {
                StopReloadTrap();
                TrySetActive(true);

                stateUI.gameObject.SetActive(false);
            }
        });
    }
    private void StopReloadTrap()
    {
        reloadTimer?.Dispose();
        stateUI.ZeroingProgressBar();
    }

    public void TrySetActive (bool value)
    {
        if (!value && currentActiveKills >= 1)
        {
            currentActiveKills--;

            return;
        }
        else if (value)
        {
            currentActiveKills = killsForOne;
        }

        isActive = value;
    }

    public void UpgradeKillsForOne (int plusValue)
    {
        killsForOne += plusValue;
        currentActiveKills = killsForOne;
    }
    public void SetKillsForOne(int value)
    {
        killsForOne = value;
        currentActiveKills = killsForOne;
    }
    public void UpgradeReloadTime(float minusValue)
    {
        reloadTime -= minusValue;
    }
    public void SetReloadTime(float value)
    {
        reloadTime = value;
    }

    public bool CanInteractive()
    {
        return !isActive;
    }
    public void Interactive()
    {
        ReloadTrap();
    }

    public void StopInteractive()
    {
        StopReloadTrap();
    }
}
