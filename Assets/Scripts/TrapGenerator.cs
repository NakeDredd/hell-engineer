using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TrapGenerator : Singleton<TrapGenerator>, IInteractive
{
    [SerializeField] private int allConnections;
    [SerializeField] private LineRenderer lineRenderer;

    private List<Turret> turrets = new List<Turret>();
    private int currentConnection = 0;
    private bool pickUp;

    public bool PickUp => pickUp;

    public bool CanInteractive()
    {
        return true;
    }

    public void Interactive()
    {
        if (!CoinManager.Instance.IsCanTakeCoins(1))
        {
            return;
        }

        CoinManager.Instance.TakeCoin(1);

        pickUp = true;

        Observable.EveryUpdate().TakeUntilDisable(gameObject).TakeWhile(x => pickUp).Finally(() => 
        {
            lineRenderer.enabled = false;

        }).Subscribe(_ => 
        {
            lineRenderer.enabled = true;

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, GlobalAI.Instance.Player.transform.position);

        });
    }

    public void StopInteractive()
    {

    }

    public void AddTrap (Turret turret)
    {
        if (currentConnection >= allConnections)
        {
            currentConnection = 0;
        }

        if (turrets.Count > currentConnection)
        {
            turrets[currentConnection].SetShoot(false);
        }
        else
        {
            turrets.Add(turret);
        }

        currentConnection++;
        pickUp = false;
    }
}
