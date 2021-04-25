using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractData : MonoBehaviour
{
    public Terms terms;
    public int bank = 0;
    public int earnings = 150;

    public int runsCompleted = 0;

    public int walkingLevel = 0;

    public int miningLevel = 0;

    public int lanternLevel = 0;
    public int merchantLevel = 0;
    public bool completed = false;
    public bool failed = false;
    void Start()
    {
        DontDestroyOnLoad(this);
    }

}
