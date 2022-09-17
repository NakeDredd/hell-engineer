using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;

    private void OnEnable()
    {
        CoinManager.OnUpdateCounter += UpdateText;
    }

    private void OnDisable()
    {
        CoinManager.OnUpdateCounter -= UpdateText;
    }

    private void UpdateText(int count)
    {
        coinText.text = count.ToString();
    }
}
