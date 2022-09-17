using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTrap : MonoBehaviour
{
    protected GameObject trapPlace;

    public void SetTrapPlace(GameObject place)
    {
        trapPlace = place;
    }

    protected void OnDestroy()
    {
        if (trapPlace == null)
        {
            return;
        }

        trapPlace.SetActive(true);
    }
}
