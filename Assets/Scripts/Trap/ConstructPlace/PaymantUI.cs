using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PaymantUI : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private string price;
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;
    [SerializeField] private SpriteRenderer coinImage;
    [SerializeField] private Sprite baseCoinSprite;
    [SerializeField] private Sprite coinSprite;

    private List<SpriteRenderer> coins = new List<SpriteRenderer>();

    private void CreateCoin (float offset, int c, int i)
    {
        SpriteRenderer coin = Instantiate(coinImage);
        coin.transform.SetParent(target.transform);

        Vector3 pos = new Vector3();
        pos.x = -offset + (xOffset * c);
        pos.y = -yOffset * i;

        coin.transform.localPosition = pos;

        coins.Add(coin);
    }

    private int[] ConvertStringToInt(string price)
    {
        char[] chars = price.ToCharArray();
        int[] prices = new int[chars.Length];

        for (int i = 0; i < chars.Length; i++)
        {
            prices[i] = (int)char.GetNumericValue(chars[i]);
        }

        return prices;
    }

    public void Init()
    {
        int[] prices = ConvertStringToInt(price);

        for (int i = 0; i < prices.Length; i++)
        {
            float offset = ((float)prices[i] / 2) * (xOffset - (xOffset / prices[i]));

            for (int y = 0; y < prices[i]; y++)
            {
                CreateCoin(offset, y, i);
            }
        }
    }
    public void Init(string price)
    {
        int[] prices = ConvertStringToInt(price);

        for (int i = 0; i < prices.Length; i++)
        {
            float offset = ((float)prices[i] / 2) * (xOffset - (xOffset / prices[i]));

            for (int y = 0; y < prices[i]; y++)
            {
                CreateCoin(offset, y, i);
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
    public void ClearSprites()
    {
        foreach (var coin in coins)
        {
            Destroy(coin.gameObject);
        }

        coins.Clear();
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
