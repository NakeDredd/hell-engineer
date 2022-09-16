using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionPlace : MonoBehaviour
{
    [SerializeField] private ConstructionPlaceUI placeUI;
    [SerializeField] private BaseTrap trap;
    [SerializeField] private Transform place;

    private GameObject player;

    private void Start()
    {
        placeUI.Init();

        placeUI.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        InputRegister.Instance.InputInteractive += PaymantCreation;
    }
    private void OnDisable()
    {
        InputRegister.Instance.InputInteractive -= PaymantCreation;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMovement player))
        {
            this.player = player.gameObject;
            placeUI.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMovement player) == this.player)
        {
            this.player = null;
            placeUI.gameObject.SetActive(false);
        }
    }

    private void PaymantCreation()
    {
        // Подключить потом кошелёк

        if (player == null)
        {
            return;
        }

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
}
