using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class DayNightCicle : MonoBehaviour
{
    [SerializeField] private Gradient gradient;
    [SerializeField] private float secondsPerDay = 120f;

    private Light2D light2d;
    private float dayTime;
    private float dayTimeSpeed;

    private void Awake()
    {
        light2d = GetComponent<Light2D>();
        dayTimeSpeed = 1 / secondsPerDay;
    }

    private void Update()
    {
        dayTime += Time.deltaTime * dayTimeSpeed;

        light2d.color = gradient.Evaluate(dayTime % 1f);
    }
}
