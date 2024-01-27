using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyWaveUI : MonoBehaviour
{
    [SerializeField] private RectTransform enemyWavePositionIndicator;
    private Vector3 indicatorDirection;
    private float indicatorOffset = 300f;
    private float distanceToSpawnPos = 0;
    private Transform mainCamera;

    [SerializeField] private EnemyWavesManager enemyWavesManager;
    [SerializeField] private TextMeshProUGUI waveIndex;
    [SerializeField] private TextMeshProUGUI timeToNextWave;
    [SerializeField] private TextMeshProUGUI timeWarning;
    private Color32 color;
    private float colorAlfa = 255;

    private bool start—ountdown = false;
    private bool hideTimer = false;
    private float time = 1;
    private void Awake()
    {
        enemyWavesManager.OnEnemyWaveIncrease += ChangeWaveIndex;
        enemyWavesManager.OnNextWaveStarting += CheckNextWaveCountdown;
        mainCamera = Camera.main.transform;
    }

    private void Update()
    {
        if (start—ountdown)
        {
            float timeToWave = enemyWavesManager.nextWaveTimer;
            if (timeToWave > 0) 
            {
                timeToNextWave.text = timeToWave.ToString("F2");
            }
            else
            {
                timeToNextWave.text = "0";
                start—ountdown = false;
                hideTimer = true;
            }
        }
        else if (hideTimer)
        {
            if(time > 0)
            {
                time -= Time.deltaTime;
                colorAlfa = Mathf.Lerp(0, 255, time);
                color.a = (byte)colorAlfa;
                timeToNextWave.color = color;
                timeWarning.color = color;
            }
            else
            {
                colorAlfa = 255;
                time = 1;
                hideTimer = false;
            }
        }

        UpdateIndicator();
    }

    private void ChangeWaveIndex(int index)
    {
        waveIndex.text = index.ToString();
    }

    private void CheckNextWaveCountdown()
    {
        start—ountdown = true;
        color.a = (byte)colorAlfa;
        timeToNextWave.color = color;
        timeWarning.color = color;
    }

    private void UpdateIndicator()
    {
        RefreshIndicatorTransform();
        CheckActive();
    }

    private void RefreshIndicatorTransform()
    {
        indicatorDirection = (enemyWavesManager.GetWaveSpawnPosition() - mainCamera.transform.position).normalized;
        enemyWavePositionIndicator.anchoredPosition = indicatorDirection * indicatorOffset;
        enemyWavePositionIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(indicatorDirection));
    }

    private void CheckActive()
    {
        distanceToSpawnPos = Vector3.Distance(enemyWavesManager.GetWaveSpawnPosition(), mainCamera.position);
        if (distanceToSpawnPos < 20)
        {
            enemyWavePositionIndicator.gameObject.SetActive(false);
        }
        else
        {
            enemyWavePositionIndicator.gameObject.SetActive(true);
        }
    }

}
