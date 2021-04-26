using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SinFlicker : MonoBehaviour
{
    public Light2D flickerTarget;
    public float intensity;

    public float intensityMultiplier = 1.0f;
    public float flickerAmp;
    public float flickerFreq;

    public float progress;
    void Update()
    {
        progress += Time.deltaTime * flickerFreq;
        flickerTarget.intensity = intensityMultiplier * (intensity + Mathf.Sin(progress) * flickerAmp);
    }
}
