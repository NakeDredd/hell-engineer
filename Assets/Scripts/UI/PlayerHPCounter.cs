using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHPCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hpText;

    private void OnEnable()
    {
        PlayerHealth.OnPlayerHpChange += UpdateText;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerHpChange -= UpdateText;
    }

    private void UpdateText (int count)
    {
        hpText.text = count.ToString();
    }
}
