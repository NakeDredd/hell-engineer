using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionPlace : MonoBehaviour, IInteractive
{
    [SerializeField] private ConstructionPlaceUI placeUI;
    [SerializeField] private BaseTrap trap;
    [SerializeField] private Transform place;

    private void Start()
    {
        placeUI.Init();

        placeUI.gameObject.SetActive(false);
    }
    //private void OnEnable()
    //{
    //    InputRegister.Instance.InputInteractive += PaymantCreation;
    //}
    //private void OnDisable()
    //{
    //    InputRegister.Instance.InputInteractive -= PaymantCreation;
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMovement player))
        {
            placeUI.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMovement player))
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
        return gameObject.activeSelf;
    }
    public void Interactive()
    {
        PaymantCreation();
    }

    public void StopInteractive()
    {
        
    }
}
