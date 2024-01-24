
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWavesManager : MonoBehaviour
{
    private enum State 
    {
        WaitingForWaveStart,
        SwitchState,
        WaitingForWaveEnd
    }
    private State currentState = State.SwitchState;

    [SerializeField] private Transform spawnZone;
    [SerializeField] private List<Transform> spawnPositions;
    private Vector3 randomOffset = Vector3.zero;
    int posIndex = 0;

    private int waveIndex = 0;
    public float nextWaveTimer { get; private set; } 
    private float timeBetweenWaves = 10;

    private float timeBetweenEnemys = 0.2f;
    private float nextEnemyTimer = 0;

    private float switchStateTime = 7;
    private float switchTimer = 1;

    private int enemysInWave = 10;
    private int enemysRemain;

    public System.Action<int> OnEnemyWaveIncrease;
    public System.Action OnNextWaveStarting;

    private void Awake()
    {
        nextWaveTimer = timeBetweenWaves;
    }
    private void Start()
    {
        GenerateWavePosition();
        nextWaveTimer = timeBetweenWaves;
    }

    private void Update()
    {
        if (currentState == State.WaitingForWaveStart) // next wave countdown 
        {
            if (nextWaveTimer > 0 )
            {
                nextWaveTimer -= Time.deltaTime;
            }
            else
            {
                GenerateNewWave();
                currentState = State.WaitingForWaveEnd;
            }
        }
        else if(currentState == State.WaitingForWaveEnd) // enemy spawning;
        {

            if (enemysRemain > 0)
            {
                if (timeBetweenEnemys < nextEnemyTimer)
                {
                    nextEnemyTimer += Time.deltaTime;
                }
                else
                {
                    randomOffset = new Vector3(Random.Range(-7f, 7f), Random.Range(-7f, 7f), 0);
                    PoolsHandler.instance.buildingDestroyers.GetObjectFromPool(spawnPositions[posIndex].position + randomOffset);
                    enemysRemain--;
                    nextEnemyTimer = 0;
                }
            }
            else
            {
                currentState = State.SwitchState;
            }
        }
        else // switch wave countdown
        {
            if(switchTimer > 0)
            {
                switchTimer -= Time.deltaTime;
            }
            else
            {
                nextWaveTimer = timeBetweenWaves;
                switchTimer = switchStateTime;
                GenerateWavePosition();
                OnNextWaveStarting?.Invoke();
                currentState = State.WaitingForWaveStart;
            }
        }
    }

    private void GenerateNewWave()
    {
        enemysInWave++;
        enemysRemain += enemysInWave;
        waveIndex++;
        OnEnemyWaveIncrease?.Invoke(waveIndex);
    }

    private void GenerateWavePosition()
    {
        posIndex = Random.Range(0, spawnPositions.Count);
        spawnZone.position = spawnPositions[posIndex].position;
    }

}
