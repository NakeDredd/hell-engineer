using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : Singleton<EnemySpawner>
{
    [SerializeField] private EnemyAIBehavior[] enemies;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private int minEnemies;
    [SerializeField] private int maxEnemies;
    [SerializeField] private float timeSpawn;
    [SerializeField] private float timeStepSpawn;

    private int currentTime = 0;
    private bool direction;

    private List<EnemyAIBehavior> currentEnemies = new List<EnemyAIBehavior>();

    private IDisposable currentTimeDis;

    private void Start ()
    {
        StartTimer();
    }

    private void StartTimer()
    {
        Observable.Timer(TimeSpan.FromSeconds(timeSpawn)).TakeUntilDisable(gameObject).Subscribe(_ =>
        {
            currentTimeDis?.Dispose();
            StartSpawn();
        });
    }
    private void StartSpawn()
    {
        int enemiesNumber = Random.Range(minEnemies, maxEnemies);
        int random = Random.Range(0, spawnPositions.Length);

        Vector3 position = spawnPositions[random].position;

        direction = random == 0 ? true : false;

        PlayerCompas.Instance.SetDirection(direction);

        int iterationNumber = 0;

        Observable.Interval(TimeSpan.FromSeconds(timeStepSpawn)).TakeWhile(x => iterationNumber < enemiesNumber).TakeUntilDisable(gameObject).Subscribe(_ =>
        {
            EnemyAIBehavior enemy = Instantiate(enemies[Random.Range(0, enemies.Length)], position, Quaternion.identity);

           currentEnemies.Add(enemy);

           iterationNumber++;
        });
    }

    public void DeleateEnemy (EnemyAIBehavior enemy)
    {
        currentEnemies.Remove(enemy);

        if (currentEnemies.Count <= 0)
        {
            ResourcesGenerator.Instance.GenerateResources(direction);

            StartTimer();
            PlayerCompas.Instance.SetDirection();
        }
    }
}
