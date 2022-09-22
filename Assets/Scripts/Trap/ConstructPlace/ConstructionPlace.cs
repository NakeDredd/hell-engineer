using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionPlace : MonoBehaviour, IInteractive
{
    [SerializeField] private PaymantUI placeUI;
    [SerializeField] private BaseTrap trap;
    [SerializeField] private Transform place;

    private void Start()
    {
        placeUI.Init();

        placeUI.gameObject.SetActive(false);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerPaymant player))
        {
            placeUI.gameObject.SetActive(false);
        }
    }

    private void PaymantCreation()
    {
        if (!CoinManager.Instance.IsCanTakeCoins(1))
        {
            return;
        }

        CoinManager.Instance.TakeCoin(1);

        if (placeUI.ChangeNextCoin())
        {
            CreateTrap();
            placeUI.RemakeSprites();
            gameObject.SetActive(false);
        }
    }
    private void CreateTrap()
    {
        BaseTrap obj = Instantiate(trap);

        obj.SetTrapPlace(gameObject);

        obj.transform.position = place.position;
    }

    public bool CanInteractive()
    {
        placeUI.gameObject.SetActive(true);

        return true;
    }

    public void Interactive()
    {
        PaymantCreation();
    }

    public void StopInteractive()
    {
    }
}
