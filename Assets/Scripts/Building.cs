using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private HealthSystem healthSystem;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            healthSystem.GetDamage(20);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            healthSystem.GetHeal(50);
        }
    }

}
