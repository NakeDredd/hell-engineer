using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class ResourcesClaster : MonoBehaviour, IInteractive
{
    [SerializeField] private TrapStateUI stateUI;
    [SerializeField] private Coin coin;
    [SerializeField] private float destroyTime;
    [SerializeField] private int minCoins;
    [SerializeField] private int maxCoins;

    private IDisposable destroyTimer;

    private void Start()
    {
        ResourcesGenerator.Instance.ClearClasters += Destroy;
    }
    private void OnDisable()
    {
        ResourcesGenerator.Instance.ClearClasters -= Destroy;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerInteraction player))
        {
            StopClasterDestroy();
            stateUI.gameObject.SetActive(false);
        }
    }

    private void ClasterDestroy()
    {
        stateUI.ZeroingProgressBar();

        float plus = 100 / destroyTime;

        float currentTime = 0;

        destroyTimer?.Dispose();

        destroyTimer = Observable.EveryFixedUpdate().TakeUntilDisable(gameObject).Subscribe(_ =>
        {
            stateUI.AddProgressBar(plus * Time.fixedDeltaTime);
            currentTime += Time.fixedDeltaTime;

            if (currentTime >= destroyTime)
            {
                StopClasterDestroy();

                int number = Random.Range(minCoins, maxCoins);

                for (int i = 0; i < number; i++)
                {
                    Instantiate(coin, transform.position + new Vector3(Random.value, Random.value), Quaternion.identity);
                }

                stateUI.gameObject.SetActive(false);
                Destroy(gameObject);
            }
        });
    }
    private void StopClasterDestroy()
    {
        destroyTimer?.Dispose();
        stateUI.ZeroingProgressBar();
    }

    public bool CanInteractive()
    {
        stateUI.gameObject.SetActive(true);

        return true;
    }
    public void Interactive()
    {
        ClasterDestroy();
    }

    public void StopInteractive()
    {
        StopClasterDestroy();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
