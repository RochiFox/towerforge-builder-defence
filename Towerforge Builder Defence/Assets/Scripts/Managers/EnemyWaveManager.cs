using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyWaveManager : MonoBehaviour
{
    public static EnemyWaveManager Instance { get; private set; }

    public event EventHandler OnWaveNumberChanged;

    private enum State
    {
        WaitingToSpawnNextWave,
        SpawningWave
    }

    [SerializeField] private Transform nextWaveSpawnPositionTransform;
    [Space]
    [SerializeField] private List<Transform> spawnPositionTransformList;

    private State state;
    private int waveNumber;
    private float nextWaveSpawnTimer;
    private float nextEnemySpawnTimer;
    private int remainingEnemySpawnAmount;

    private Vector3 spawnPosition;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        state = State.WaitingToSpawnNextWave;
        spawnPosition = spawnPositionTransformList[Random.Range(0, spawnPositionTransformList.Count)].position;
        nextWaveSpawnPositionTransform.position = spawnPosition;

        nextWaveSpawnTimer = 3f;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToSpawnNextWave:
                nextWaveSpawnTimer -= Time.deltaTime;

                if (nextWaveSpawnTimer < 0f)
                    SpawnWave();

                break;
            case State.SpawningWave:
                if (remainingEnemySpawnAmount > 0)
                {
                    nextEnemySpawnTimer -= Time.deltaTime;

                    if (nextEnemySpawnTimer < 0f)
                    {
                        nextEnemySpawnTimer = Random.Range(0f, 0.2f);

                        Enemy.Create(spawnPosition + UtilsClass.GetRandomDirection() * Random.Range(0f, 10f));
                        remainingEnemySpawnAmount--;

                        if (remainingEnemySpawnAmount <= 0f)
                        {
                            state = State.WaitingToSpawnNextWave;
                            spawnPosition = spawnPositionTransformList[Random.Range(0, spawnPositionTransformList.Count)].position;
                            nextWaveSpawnPositionTransform.position = spawnPosition;
                            nextWaveSpawnTimer = 15f;
                        }
                    }
                }

                break;
        }
    }

    private void SpawnWave()
    {
        remainingEnemySpawnAmount = 3 + 2 * waveNumber;

        state = State.SpawningWave;
        waveNumber++;

        OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetWaveNumber() => waveNumber;

    public float GetNextWaveSpawnTimer() => nextWaveSpawnTimer;

    public Vector3 GetSpawnPosition() => spawnPosition;
}
