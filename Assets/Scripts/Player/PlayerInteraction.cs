using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private List<IInteractive> places = new List<IInteractive>();

    private void Start()
    {
        InputRegister.Instance.InputInteractive += Interactive;
        InputRegister.Instance.UntapInteractive += StopInteractive;
    }
    private void OnDisable()
    {
        InputRegister.Instance.InputInteractive -= Interactive;
        InputRegister.Instance.UntapInteractive -= StopInteractive;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractive interactive))
        {
            places.Remove(interactive);
        }
    }

    private void Interactive ()
    {
        if (places.Count <= 0)
        {
            return;
        }

        places[0].Interactive();

        if (places.Count > 0 && !places[0].CanInteractive())
        {
            places.RemoveAt(0);
        }
    }
    private void StopInteractive()
    {
        if (places.Count <= 0)
        {
            return;
        }

        places[0].StopInteractive();
    }

    public bool GetInteractive (IInteractive interact)
    {
        if (interact.CanInteractive())
        {
            places.Add(interact);
        }

        if (places.Count <= 0 || places[0] != interact)
        {
            return false;
        }

        return true;
    }
}

public interface IInteractive
{
    public bool CanInteractive();
    public void Interactive();
    public void StopInteractive();
}
