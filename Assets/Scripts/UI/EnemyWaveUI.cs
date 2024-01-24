using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyWaveUI : MonoBehaviour
{
    [SerializeField] private EnemyWavesManager enemyWavesManager;
    [SerializeField] private TextMeshProUGUI waveIndex;
    [SerializeField] private TextMeshProUGUI timeToNextWave;
    [SerializeField] private TextMeshProUGUI timeWarning;
    [SerializeField] private Color32 color;
    [SerializeField] private float colorAlfa = 255;

    [SerializeField] private bool start—ountdown = false;
    [SerializeField] private bool hideTimer = false;
    [SerializeField] private float time = 1;
    private void Awake()
    {
        enemyWavesManager.OnEnemyWaveIncrease += ChangeWaveIndex;
        enemyWavesManager.OnNextWaveStarting += CheckNextWaveCountdown;
    }

    private void Update()
    {
        if (start—ountdown)
        {
            float timeToWave = enemyWavesManager.nextWaveTimer;
            Debug.Log("float timeToWave = " + timeToWave);
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
}
