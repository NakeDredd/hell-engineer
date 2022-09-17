using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Coin : MonoBehaviour
{
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
