using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class HealthSystem : MonoBehaviour
{
    private int healthAmount = 0;
    [SerializeField] private int healthAmountMax = 100;

    public event EventHandler<OnHealthChangedEventHandler> OnHealthChangedEvent;
    public event EventHandler OnDiedEvent;
    public class OnHealthChangedEventHandler : EventArgs
    {
        public float deltaNormilizeValue;
        public float afterNormilizeValue;
    }

    [SerializeField] private bool canBeRecover = true;
    [SerializeField] private float time = 0;
    private float recoverTime = 3f;

    private void Awake()
    {
        healthAmount = healthAmountMax;
    }

    public void GetDamage(int damageAmount)
    {
        int beforHealth = healthAmount;
        healthAmount -= damageAmount;


        time = 0;
        if (canBeRecover == true)
        {
            canBeRecover = false;
            StartCoroutine(RecoverTimer());
        }

        if(healthAmount <0)
        {
            healthAmount = 0;
            Debug.Log("Smert");
            OnDiedEvent?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);
        }

        OnHealthChangedEvent?.Invoke(this, new OnHealthChangedEventHandler{
            afterNormilizeValue = healthAmount / (float)healthAmountMax,
            deltaNormilizeValue = damageAmount / (float)healthAmountMax
            
        });
    }

    public void GetHeal(int healAmount)
    {
        if(healthAmount < healthAmountMax && canBeRecover == true)
        {
            healAmount = Mathf.Clamp(healAmount, 0, healthAmountMax - healthAmount);
            healthAmount += healAmount;
            OnHealthChangedEvent?.Invoke(this, new OnHealthChangedEventHandler
            {
                afterNormilizeValue = healthAmount / (float)healthAmountMax,
                deltaNormilizeValue = -healAmount / (float)healthAmountMax
            });
        }
    }

    IEnumerator RecoverTimer()
    {
        while(time < recoverTime)
        {
            time += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        canBeRecover = true;
        yield break;
    }

    public bool IsDead()
    {
        return healthAmount == 0;
    }
    public bool IsFullHealth()
    {
        return healthAmount == healthAmountMax;
    }
    public int GetHealthAmount()
    {
        return healthAmount;
    }


    public float GetHealthAmountNormalize()
    {
        return healthAmount / (float)healthAmountMax;
    }


}
