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
    [SerializeField] private SpriteRenderer[] indicators;
    [SerializeField] private TMP_Text currentTimeText;
    [SerializeField] private int minEnemies;
    [SerializeField] private int maxEnemies;
    [SerializeField] private float timeSpawn;
    [SerializeField] private float timeStepSpawn;

    private int currentTime = 0;

    private List<EnemyAIBehavior> currentEnemies = new List<EnemyAIBehavior>();

    private IDisposable currentTimeDis;

    private void Start ()
    {
        StartCurrentTime();
        StartTimer();
    }

    private void StartCurrentTime ()
    {
        currentTimeDis = Observable.Interval(TimeSpan.FromSeconds(1)).TakeUntilDisable(gameObject).Subscribe(_ =>
        {
            currentTime++;
            currentTimeText.text = currentTime.ToString();
        });
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
        SetIndicator(random, Color.green);

        int iterationNumber = 0;

        Observable.Timer(TimeSpan.FromSeconds(timeStepSpawn)).Repeat().TakeWhile(x => iterationNumber < enemiesNumber).TakeUntilDisable(gameObject).Subscribe(_ =>
        {
            EnemyAIBehavior enemy = Instantiate(enemies[Random.Range(0, enemies.Length)], position, Quaternion.identity);

           currentEnemies.Add(enemy);

           iterationNumber++;
        });
    }

    private void SetIndicator (int i, Color color)
    {
        indicators[i].color = color;
    }

    public void DeleateEnemy (EnemyAIBehavior enemy)
    {
        currentEnemies.Remove(enemy);

        if (currentEnemies.Count <= 0)
        {
            StartCurrentTime();
            StartTimer();
            SetIndicator(0, Color.white);
            SetIndicator(1, Color.white);
        }
    }
}
