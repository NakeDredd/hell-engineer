using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRegister : Singleton<InputRegister>
{
    public Action<float> InputMovement;
    public Action<bool> InputCompass, UntapCompass;
    public Action InputInteractive, UntapInteractive;
    public Action InputPaymant;
    public Action InputPause;

    private void Update()
    {
        InputMovement?.Invoke(Input.GetAxis("Horizontal"));

        if (Input.GetKeyDown(KeyCode.E))
        {
            InputInteractive?.Invoke();
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            UntapInteractive?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            InputCompass?.Invoke(true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            UntapCompass?.Invoke(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InputPause?.Invoke();
        }
    }
}
