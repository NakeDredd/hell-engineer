using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionPlaceUI : MonoBehaviour
{
    [SerializeField] private GameObject[] grids;
    [SerializeField] private int[] gridsPrice;
    [SerializeField] private Image coinImage;
    [SerializeField] private Sprite baseCoinSprite;
    [SerializeField] private Sprite coinSprite;

    private List<Image> coins = new List<Image>();

    public void Init()
    {
        for (int g = 0; g < grids.Length; g++)
        {
            for (int i = 0; i < gridsPrice[g]; i++)
            {
                Image coin = Instantiate(coinImage);
                coin.transform.SetParent(grids[g].transform);

                coins.Add(coin);
            }
        }
    }
    public void RemakeSprites()
    {
        foreach (var coin in coins)
        {
            coin.sprite = baseCoinSprite;
        }
    }

    public bool ChangeNextCoin ()
    {
        foreach (var image in coins)
        {
            if (image.sprite != coinSprite)
            {
                image.sprite = coinSprite;

                if (image != coins.Last())
                {
                    return false;
                }
            }
        }

        return true;
    }
}
