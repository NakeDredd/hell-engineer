using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TrapState : MonoBehaviour
{
    [SerializeField] private TrapStateUI stateUI;
    [SerializeField] private float reloadTime;

    private GameObject player;
    private IDisposable reloadTimer;
    private bool isActive = true;

    public bool IsActive => isActive;


    private void Start()
    {
        InputRegister.Instance.InputInteractive += ReloadTrap;
        InputRegister.Instance.UntapInteractive += StopReloadTrap;
    }
    private void OnDisable()
    {
        InputRegister.Instance.InputInteractive -= ReloadTrap;
        InputRegister.Instance.UntapInteractive -= StopReloadTrap;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMovement player) && !isActive)
        {
            this.player = player.gameObject;
            stateUI.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMovement player) == this.player && !isActive)
        {
            this.player = null; 
            StopReloadTrap();
            stateUI.gameObject.SetActive(false); 
        }
    }

    private void ReloadTrap()
    {
        if (player == null)
        {
            return;
        }

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
}
