using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCicle : MonoBehaviour
{
    [SerializeField] Gradient gradient;
    private Light2D light2D;
    private float dayTime = 140f;
    private float currentTime = 0;
    private float dayTimeSpeed = 5f;

    private void Awake()
    {
        dayTimeSpeed = 1 / dayTime;
        light2D = GetComponent<Light2D>();
    }

    private void Update()
    {
        currentTime += dayTimeSpeed * Time.deltaTime;
        light2D.color = gradient.Evaluate(currentTime);
    }
}
