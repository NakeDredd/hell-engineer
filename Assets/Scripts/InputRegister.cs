using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRegister : Singleton<InputRegister>
{
    public Action<float> InputMovement;

    private void Update()
    {
        InputMovement.Invoke(Input.GetAxis("Horizontal"));
    }
}
