using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SinFlicker : MonoBehaviour
{
    public Light2D flickerTarget;
    public float intensity;
    public float flickerAmp;
    public float flickerFreq;
    void Update()
    {
        flickerTarget.intensity = intensity + Mathf.Sin(Time.timeSinceLevelLoad * flickerFreq) * flickerAmp;
    }
}
