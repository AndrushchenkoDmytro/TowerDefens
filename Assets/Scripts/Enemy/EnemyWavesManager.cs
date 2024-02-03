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
    private List<int> spawnPoints;
    private float offset;
    private Vector3 randomOffset = Vector3.zero;
    int posIndex = 0;

    private int waveIndex = 0;
    public float nextWaveTimer { get; private set; } 
    private float timeBetweenWaves = 12;

    private float timeBetweenEnemys = 0.25f;
    private float nextEnemyTimer = 0;

    private float switchStateTime = 7;
    private float switchTimer = 1;

    private int enemysInWave = 1;
    private int enemysRemain;

    public System.Action<int> OnEnemyWaveIncrease;
    public System.Action OnNextWaveStarting;

    private void Awake()
    {
        nextWaveTimer = timeBetweenWaves;
        GameObject.Find("MainTower").GetComponent<MainTower>().OnGameOver += () => { gameObject.SetActive(false); };
    }
    private void Start()
    {
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
                    randomOffset = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
                    if(waveIndex < 25)
                    {
                        if(Random.Range(1,20f) < 14) 
                            PoolsHandler.instance.buildingDestroyers.GetObjectFromPool(spawnZone.position + randomOffset);
                        else
                            PoolsHandler.instance.resourceDestroyers.GetObjectFromPool(spawnZone.position + randomOffset);
                    }
                    else if(waveIndex < 50)
                    {
                        if (Random.Range(1, 40f) < 20)
                            PoolsHandler.instance.buildingDestroyers.GetObjectFromPool(spawnZone.position + randomOffset);
                        else if(Random.Range(1, 40f) < 32)
                            PoolsHandler.instance.resourceDestroyers.GetObjectFromPool(spawnZone.position + randomOffset);
                        else 
                            PoolsHandler.instance.mainTowerDestroyers.GetObjectFromPool(spawnZone.position + randomOffset);
                    }
                    else
                    {
                        if (Random.Range(1, 60f) < 15)
                            PoolsHandler.instance.mainTowerDestroyers.GetObjectFromPool(spawnZone.position + randomOffset);
                        else if (Random.Range(1, 60f) < 50)
                            PoolsHandler.instance.resourceDestroyers.GetObjectFromPool(spawnZone.position + randomOffset);
                        else
                            PoolsHandler.instance.mainTowerDestroyers.GetObjectFromPool(spawnZone.position + randomOffset);
                    }
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

        posIndex = Random.Range(0, spawnPoints.Count);
        if (posIndex % 2 != 0 && posIndex != spawnPoints.Count) posIndex++;
        Mathf.Clamp(posIndex,0, spawnPoints.Count-1);
        spawnZone.position = new Vector3(spawnPoints[posIndex]*offset, spawnPoints[posIndex + 1]*offset, 0);
    }

    public Vector3 GetWaveSpawnPosition()
    {
        return spawnZone.position;
    }

    public int GetWaveIndex()
    {
        return waveIndex;
    }

    public void SetSpawnPoints(ref int[,] points, int offset)
    {
        this.offset = offset;
        spawnPoints = new List<int>();
        for (int i = 3; i < 10; i++)
        {
            for (int j = 3; j < 10; j++)
            {
                if (points[i, j] == 0)
                {
                    spawnPoints.Add(i-6);
                    spawnPoints.Add(j-6);
                }
            }
        }
    }
}
