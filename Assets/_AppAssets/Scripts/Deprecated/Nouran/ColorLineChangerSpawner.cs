using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLineChangerSpawner : MonoBehaviour
{
    public Transform SpawnPosition;
    public float SpawnRate;

    private GameObject _spawnedObj;
    private GameplayColor _gameplayColor;

    private void Start()
    {
        //Spawn();
    }

    public void Spawn()
    {
        _gameplayColor = RandomizeGamePlayColor();

        if(_gameplayColor == GameplayColor.Green)
          _spawnedObj = ObjectPooler.Instance.SpwanFromPool("green", SpawnPosition.position, Quaternion.identity);
 
        else if(_gameplayColor == GameplayColor.Red)
          _spawnedObj = ObjectPooler.Instance.SpwanFromPool("red", SpawnPosition.position, Quaternion.identity);

        else
          _spawnedObj = ObjectPooler.Instance.SpwanFromPool("yellow", SpawnPosition.position, Quaternion.identity);
    }

    public GameplayColor RandomizeGamePlayColor()
    {
        return (GameplayColor)Random.Range(0, 3);
    }

    
}
