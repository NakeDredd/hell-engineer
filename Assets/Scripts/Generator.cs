using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private float chargeTime;
    [SerializeField] private GameObject coinPrefab;

    private void Start()
    {
        Observable.Interval(TimeSpan.FromSeconds(chargeTime)).Subscribe(_=>
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        });
    }
}
