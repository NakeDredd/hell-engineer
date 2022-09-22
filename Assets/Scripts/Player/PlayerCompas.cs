using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCompas : Singleton<PlayerCompas>
{
    [SerializeField] private GameObject compass;
    [SerializeField] private Image leftSprite;
    [SerializeField] private Image rightSprite;

    private void Start()
    {
        InputRegister.Instance.InputCompass += compass.SetActive;
        InputRegister.Instance.UntapCompass += compass.SetActive;
    }
    private void OnDestroy()
    {
        InputRegister.Instance.InputCompass -= compass.SetActive;
        InputRegister.Instance.UntapCompass -= compass.SetActive;
    }

    private void SetLeftSprite (bool value)
    {
        if (value)
        {
            leftSprite.color = Color.red;
        }
        else
        {
            leftSprite.color = Color.white;
        }
    }
    private void SetrightSprite(bool value)
    {
        if (value)
        {
            rightSprite.color = Color.red;
        }
        else
        {
            rightSprite.color = Color.white;
        }
    }
    public void SetDirection (bool? left = null)
    {
        if (left == true)
        {
            SetLeftSprite(true);
            SetrightSprite(false);
        }
        else if (left == false)
        {
            SetLeftSprite(false);
            SetrightSprite(true);
        }
        else
        {
            SetLeftSprite(false);
            SetrightSprite(false);
        }
    }
}
