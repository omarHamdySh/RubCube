using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinHandller : MonoBehaviour
{
    [SerializeField] private int coinValue;
    [SerializeField] private CoinType coinType;
    private bool isCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(ImportantData.PlayerTag) && !isCollected)
        {
            isCollected = true;
            if (coinType == CoinType.Gold)
            {
                GameManager.Instance.gold += coinValue;
                LevelUI.instance.SetGold(GameManager.Instance.gold);
            }
            else if(coinType==CoinType.Crystal)
            {
                GameManager.Instance.crystal += coinValue;
                LevelUI.instance.SetCrystal(GameManager.Instance.crystal);
            }
            playCoinsCollectionSound();
            Destroy(gameObject);
        }
    }

    public void playCoinsCollectionSound()
    {
        GameManager.Instance.sfxSource.clip = GameManager.Instance.coinCollectionAudioClip;
        GameManager.Instance.sfxSource.Play();
    }
}

public enum CoinType
{
    Gold,
    Crystal
}