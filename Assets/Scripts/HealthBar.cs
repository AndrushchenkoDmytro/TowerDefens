using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static HealthSystem;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private Transform healthBarProgress;
    [SerializeField] private Transform healthBarFG;
    [SerializeField] private SpriteRenderer healthBarProgressSprite;
    [SerializeField] private Color32 reduceHealthProgressColor;
    [SerializeField] private Color32 increaseHealthProgressColor;
    [SerializeField] private float currentHealth;
    [SerializeField] private float targetHealth;
    [SerializeField] private float healTarget;

    private bool isStateSwitched = false;
    private bool isHealthReduce = true;
    private bool isChangeHealthCoroutineEnd = true;
    private float time = 1;


    private void Awake()
    {
        healthSystem.OnHealthChangedEvent += ChangeHealthInBar;
        HideHealthBar();
        currentHealth = healthBarProgress.localScale.x;
    }

    public void ChangeHealthInBar(object sender, OnHealthChangedEventHandler e)
    {
        if(e.deltaNormilizeValue > 0) // health system get damage 
        {
            if (healthBarFG.localScale.x == 1) ShowHealthBar();

            time = 1;
            currentHealth = healthBarFG.localScale.x;
            targetHealth = healthSystem.GetHealthAmountNormalize();
            healthBarFG.localScale = new Vector3(targetHealth, 1, 1);

            if(isHealthReduce == false)
            {
                isHealthReduce = true;
                isStateSwitched = true;
            }

            if (isChangeHealthCoroutineEnd == true)
            {
                isChangeHealthCoroutineEnd = false;
                StartCoroutine(ChangeHealthCoroutine());
            }
        }
        else
        {
            time = 0;
            currentHealth = healthBarFG.localScale.x;
            targetHealth = healthSystem.GetHealthAmountNormalize();
            healthBarProgress.localScale = new Vector3(targetHealth, 1, 1);
            healTarget = targetHealth;

            if(isHealthReduce == true)
            {
                healthBarProgressSprite.color = increaseHealthProgressColor;
            }

            isHealthReduce = false;
            if (isChangeHealthCoroutineEnd == true)
            {
                isChangeHealthCoroutineEnd = false;
                StartCoroutine(ChangeHealthCoroutine());
            }
        }
    }

    IEnumerator ChangeHealthCoroutine()
    {
        while (true)
        {
            if (isStateSwitched)
            {

                healthBarProgress.localScale = new Vector3(healTarget, 1, 1);
                isStateSwitched = false;
                healthBarProgressSprite.color = reduceHealthProgressColor;
                Debug.Log("Switching");
                yield return new WaitForSeconds(0.075f);
            }
            else if (isHealthReduce)
            {
                time -= Time.deltaTime;
                healthBarProgress.localScale = new Vector3(Mathf.Lerp(targetHealth, currentHealth, time), 1, 1);
                if(healthBarProgress.localScale.x == targetHealth)
                {
                    isChangeHealthCoroutineEnd = true;
                    yield break;
                }
                Debug.Log("Reduce");
                yield return new WaitForFixedUpdate();
            }
            else
            {
                time += Time.deltaTime * 1.5f;
                healthBarFG.localScale = new Vector3(Mathf.Lerp(currentHealth, targetHealth, time), 1, 1);
                if (healthBarFG.localScale.x == targetHealth)
                {
                    if (targetHealth == 1) HideHealthBar();

                    healthBarProgressSprite.color = reduceHealthProgressColor;
                    isHealthReduce = true;
                    isChangeHealthCoroutineEnd = true;
                    yield break;
                }
                yield return new WaitForFixedUpdate();
            }
        }
    }

    private void ShowHealthBar()
    {
        gameObject.SetActive(true);
    }
    private void HideHealthBar()
    {
        gameObject.SetActive(false);
    }

}
