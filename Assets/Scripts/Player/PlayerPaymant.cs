using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaymant : MonoBehaviour
{
    private List<IPaymant> paymants = new List<IPaymant>();

    private void Start()
    {
        InputRegister.Instance.InputPaymant += Paymant;
    }
    private void OnDestroy()
    {
        InputRegister.Instance.InputPaymant -= Paymant;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IPaymant iPaymant) && iPaymant.CanPaymant())
        {
            paymants.Add(iPaymant);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IPaymant iPaymant))
        {
            paymants.Remove(iPaymant);
        }
    }

    private void Paymant ()
    {
        if (paymants.Count == 0)
        {
            return;
        }

        paymants[0].Paymant();

        if (paymants.Count > 0 && !paymants[0].CanPaymant())
        {
            paymants.RemoveAt(0);
        }
    }
}

public interface IPaymant
{
    public void Paymant();
    public bool CanPaymant();
}
