using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class FadeOverTime : MonoBehaviour
{
    private Light2D target;
    public float lifetime;
    private float initialIntensity;
    private float timer;
    
    private void Start() 
    {
        target = GetComponent<Light2D>();
        initialIntensity = target.intensity;
    }

    private void Update() 
    {
        timer += Time.deltaTime;
        target.intensity = initialIntensity * (1.0f - timer/lifetime);
        if (timer >= lifetime)
        {
            timer-= lifetime;
        }
    }
}
