using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapStateUI : MonoBehaviour
{
    [SerializeField] private Image progressBar;

    public void AddProgressBar (float value)
    {
        progressBar.fillAmount += value / 100;
    }

    public void ZeroingProgressBar ()
    {
        progressBar.fillAmount = 0;
    }
}
