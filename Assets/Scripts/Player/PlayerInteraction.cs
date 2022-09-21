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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractive interactive) && interactive.CanInteractive())
        {
            places.Add(interactive);
        }
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
            places[0].StopInteractive();
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
}

public interface IInteractive
{
    public bool CanInteractive(); // Внутрення настройка объекта по типу отображения UI и прочие проверки
    public void Interactive();  // Взаимодействие
    public void StopInteractive(); // Преркащение взаимодействия, например зажима кнопки
}
