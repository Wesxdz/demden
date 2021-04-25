using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractData : MonoBehaviour
{
    public Terms terms;
    public int bank = 0;
    public int earnings = 150;

    public int runsCompleted = 0;

    public Dictionary<PlayerSkill, int> levels = new Dictionary<PlayerSkill, int>();
    public bool completed = false;
    public bool failed = false;
    void Start()
    {
        levels[PlayerSkill.MoveRate] = 1;
        levels[PlayerSkill.MineSpeed] = 1;
        levels[PlayerSkill.Lantern] = 1;
        levels[PlayerSkill.Negotiation] = 1;
        DontDestroyOnLoad(this);
    }

}
