using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TrapGenerator : Singleton<TrapGenerator>, IInteractive
{
    [SerializeField] private LineRenderer lineRenderer;

    private List<Turret> turrets = new List<Turret>();
    private PrefsValue<int> allConnections;
    private int currentConnection = 0;
    private bool pickUp;

    public bool PickUp => pickUp;
    private void Start()
    {
        allConnections = new PrefsValue<int>("AllConnections", 2);

        TrapGeneratorUpgrater.UpgradeEvent += Upgrade;
    }
    private void OnDestroy()
    {
        TrapGeneratorUpgrater.UpgradeEvent -= Upgrade;
    }
    public bool CanInteractive()
    {
        return true;
    }

    public void Interactive()
    {
        if (!CoinManager.Instance.IsCanTakeCoins(1) || pickUp)
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
        if (currentConnection >= allConnections.Value)
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
    private void Upgrade(UpgradeVariant up, float value)
    {
        switch (up)
        {
            case UpgradeVariant.DAMAGE:
                {

                }
                break;
            case UpgradeVariant.RANGE_ZONE:
                {
                    allConnections.Value = (int)value;
                }
                break;
            case UpgradeVariant.RATE_OF_FIRE:
                {

                }
                break;
            case UpgradeVariant.KILLS_FOR_ONE:
                {
                    Debug.Log("мер рср мхусъ");
                }
                break;
        }
    }

}
