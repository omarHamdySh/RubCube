using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinLine : MonoBehaviour
{
    public GameObject Coin;
    public Transform  CoinSpawnPoint;
    public int MaxCoinNumber;
    public float CoinsSpacingFactor;
    

    public void Init()
    {
        SpawnCoins();
    }

    public void SpawnCoins()
    {
        int NumberOfCoins = Random.Range(0, MaxCoinNumber);
        
        for (int i = 0; i < NumberOfCoins; i++)
        {
            var coin = Instantiate(Coin, CoinSpawnPoint);
            coin.transform.localPosition = new Vector3(0, 0, i * CoinsSpacingFactor);
        }

    }
}
