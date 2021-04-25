using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
The Smasher is responsible for each demon's patterns of opening and closing their mouth
*/
public class Smasher : MonoBehaviour
{
    public Transform crush;
    public AudioSource crushVFX;
    public float crushWarningTime = 0.5f;
    private bool warnedCrush = false;
    public AudioSource cleanOST;
    public float teethSeparationDistance;
    public float closeTime = 0.5f;
    public float holdClosedTime = 2.0f;
    public float openTime = 1.0f;
    public AnimationCurve openRate;
    public AnimationCurve closeRate;
    // Trigger, when should the demon close mouth?
    // How fast should the demon close their mouth
    public enum MouthState
    {
        Open,
        Closing,
        Closed,
        Opening
    }
    public float minOpenTime;
    public float maxOpenTime;
    private float stateTimeStarted;
    private float stateTimeRemaining;
    public float extraSpawnOpenTime = 5.0f;
    public MouthState mouthState = MouthState.Open;

    private void Start() {
        SetStateTime(extraSpawnOpenTime + Random.Range(minOpenTime, maxOpenTime)); 
    }

    void FixedUpdate()
    {
        stateTimeRemaining -= Time.deltaTime;
        bool changedState = false;
        if (stateTimeRemaining <= 0)
        {
            stateTimeRemaining = 0;
            changedState = true;
        }
        if (mouthState == MouthState.Closing)
        {
            crush.localPosition = new Vector3(crush.localPosition.x, -teethSeparationDistance * closeRate.Evaluate(1.0f - stateTimeRemaining/stateTimeStarted), 0.0f);
        } 
        else if (mouthState == MouthState.Opening)
        {
            crush.localPosition = new Vector3(crush.localPosition.x, -teethSeparationDistance * openRate.Evaluate(stateTimeRemaining/stateTimeStarted), 0.0f);
            cleanOST.volume = openRate.Evaluate(1.0f - stateTimeRemaining/stateTimeStarted);
        } else if (mouthState == MouthState.Open)
        {
            if (!warnedCrush && stateTimeRemaining < crushWarningTime)
            {
                crushVFX.Play();
                warnedCrush = true;
            }
        }
        if (changedState)
        {
            mouthState = (Smasher.MouthState)(((int)(mouthState + 1)) % 4);
            if (mouthState == MouthState.Open)
            {
                SetStateTime(Random.Range(minOpenTime, maxOpenTime));
            } else if (mouthState == MouthState.Closing)
            {
                // Trigger death rather than actually physically smashing
                // mouth.teethOnTop.transform.GetComponent<TilemapCollider2D>().isTrigger = true;
                cleanOST.volume = 0.0f;
                SetStateTime(closeTime);
            } else if (mouthState == MouthState.Closed)
            {
                //mouth.teethOnTop.transform.GetComponent<TilemapCollider2D>().isTrigger = false;
                warnedCrush = false;
                SetStateTime(holdClosedTime);   
            } else if (mouthState == MouthState.Opening)
            {

                SetStateTime(openTime);
            }
        }
    }

    private void SetStateTime(float time)
    {
        stateTimeStarted = stateTimeRemaining = time;
    }
}
