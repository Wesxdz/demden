using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerAbilities : MonoBehaviour
{
    private ContractData contract;
    public Animator animator;
    public PlayerMovement movement;
    public SinFlicker latern;
    private void Start() 
    {
        contract = FindObjectOfType<ContractData>();
    }

    private void Update() 
    {
        UpdateSkills();    
    }
    public void UpdateSkills()
    {
        animator.SetFloat("MineSpeed", 1.0f + contract.levels[PlayerSkill.MineSpeed] * 0.2f);
        movement.moveRate = 1/(15.0f + contract.levels[PlayerSkill.MoveRate] * 3);
        latern.intensity = 0.03f + contract.levels[PlayerSkill.Lantern] * 0.01f;
    }
}
