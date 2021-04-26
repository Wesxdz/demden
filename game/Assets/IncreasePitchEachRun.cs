using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasePitchEachRun : MonoBehaviour
{
    void Start()
    {
        GetComponent<AudioSource>().pitch = 1.0f + FindObjectOfType<ContractData>().runsCompleted * 0.01f;
    }
}
