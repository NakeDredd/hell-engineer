using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Coin : MonoBehaviour
{
    [SerializeField] private float time = 0.5f;

    private void Start()
    {
        Observable.Timer(System.TimeSpan.FromSeconds(time)).TakeUntilDisable(gameObject).Subscribe(_ => 
        {
            gameObject.layer = 7;
        });
    }

    public void Collect()
    {
        CoinManager.Instance.AddCoin();
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out PlayerMovement _))
        {
            Collect();
        }
    }

}
