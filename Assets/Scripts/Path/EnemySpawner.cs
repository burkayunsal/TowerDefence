using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Singleton<EnemySpawner>
{
    [SerializeField] private Transform enemyParent;
    [SerializeField] private float enemySpawnPerSecondsMin;
    [SerializeField] private float enemySpawnPerSecondsMax;
    [SerializeField] public float timeToReachMaxSpawnRate;
    
    private float nextSpawnSeconds,timePassedSinceStart,totalSpawnChance;
    private void Update()
    {
        if (GameManager.isRunning)
        {
            timePassedSinceStart += Time.deltaTime;
            if (timePassedSinceStart >= nextSpawnSeconds)
            {
                float spawnRateRampUpPercentage = timePassedSinceStart / timeToReachMaxSpawnRate;
                float currentEnemyPerSeconds = enemySpawnPerSecondsMin + ((enemySpawnPerSecondsMax - enemySpawnPerSecondsMin) * spawnRateRampUpPercentage);
                nextSpawnSeconds = timePassedSinceStart + (1 / currentEnemyPerSeconds);
                SpawnEnemyOnPath();
            } 
        }
    }

    void SpawnEnemyOnPath()
    {
        Node startNode = PathManager.I.GetStartNode();
        Enemy enemy = PoolHandler.I.GetObject<Enemy>();
        enemy.transform.position = startNode.transform.position;
        enemy.transform.SetParent(enemyParent);
        BehaviourController controller = enemy.GetComponent<BehaviourController>();
       
        if (controller.TryGetBehaviour<PathMovement>(out var pathMovementBehaviour))
        {
            pathMovementBehaviour.SetNode(startNode);
        }
    }
}
