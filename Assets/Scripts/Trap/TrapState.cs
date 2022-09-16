using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapState : MonoBehaviour
{
    [SerializeField] private BaseTrap baseTrap;

    private bool isActive = true;

    public void SetActive (bool value)
    {
        isActive = value;
    }
}
