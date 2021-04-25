using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Terms
{
    public List<Objective> objectives = new List<Objective>();
    public int depth;
    public int reward;
}

public class Contract : MonoBehaviour
{

    public TextMeshProUGUI bankLabel;
    public TextMeshProUGUI depthLabel;
    public TextMeshProUGUI rewardLabel;
    public TextMeshProUGUI objectiveQuantityLabel;

    private void Start() 
    {
        ContractData data = FindObjectOfType<ContractData>();
        if (data.completed)
        {
            data.bank += data.terms.reward; // TODO: Extra earnings from in level
            bankLabel.text = "$" + data.bank.ToString();
            data.runsCompleted++;
            data.completed = false;
        } 
        else if (data.failed)
        {
            data.bank = 0;
            bankLabel.text = "$" + data.bank.ToString();
            data.runsCompleted = 0;
            // Reset runs but not levels on death
            data.failed = false;
        }
        data.terms = GenerateContractTerms();
        SetUIFromTerms(data.terms);
    }

    private Terms GenerateContractTerms()
    {
        Terms terms = new Terms();
        ContractData data = FindObjectOfType<ContractData>();
        terms.depth = 5 + data.runsCompleted * 5;
        var objective = new Objective();
        objective.type = ObjectiveType.ExtractSmallCavity;
        if (data.runsCompleted == 0)
        {
            objective.quantity = 1;
            terms.reward = 100;
        } 
        else
        {
            objective.quantity = 2 + (int)(data.runsCompleted * 0.25f) + Random.Range(0, (int)(data.runsCompleted/2.0f));
            terms.reward = (int)((objective.quantity * (50 + Random.Range(-10.0f, 10.0f) * (int)(terms.depth/10.0f)) * (data.levels[PlayerSkill.Negotiation] * 0.1f + Random.Range(0.8f, 1.3f))));
        }
        terms.objectives.Add(objective);
        return terms;
    }
    public void EnterMouth()
    {
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
    }

    public void SetUIFromTerms(Terms terms)
    {
        depthLabel.text = terms.depth + "m";
        rewardLabel.text = "$" + terms.reward;
        objectiveQuantityLabel.text = "x" + terms.objectives[0].quantity;
    }
}
