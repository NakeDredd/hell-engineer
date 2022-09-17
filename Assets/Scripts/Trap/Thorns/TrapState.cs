using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TrapState : MonoBehaviour, IInteractive
{
    [SerializeField] private TrapStateUI stateUI;
    [SerializeField] private float reloadTime;

    private IDisposable reloadTimer;
    private bool isActive = true;

    public bool IsActive => isActive;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMovement player) && !isActive)
        {
            stateUI.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMovement player) && !isActive)
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
                SetActive(true);

                stateUI.gameObject.SetActive(false);
            }
        });
    }
    private void StopReloadTrap()
    {
        reloadTimer?.Dispose();
        stateUI.ZeroingProgressBar();
    }

    public void SetActive (bool value)
    {
        isActive = value;
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
