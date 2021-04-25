using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ObjectiveType
{
    ExtractSmallCavity
};

[System.Serializable]
public struct Objective
{
    public ObjectiveType type;

    public int quantity;
};

public class ClientTask : MonoBehaviour
{

    public bool objectivesComplete = false;
    public Transform objectivesHUD;
    public GameObject objectiveIndicatorPrefab;

    public UnityEvent OnObjectiveCollected;
    public UnityEvent OnAllObjectivesCollected;

    [System.Serializable]
    public struct ObjectiveData
    {
        public ObjectiveType type;
        public Sprite icon;
    }

    public List<ObjectiveData> objectiveData;

    public List<Objective> objectives;
    private Dictionary<ObjectiveType, int> remaining = new Dictionary<ObjectiveType, int>();

    private Dictionary<ObjectiveType, ObjectiveIndicator> indicators = new Dictionary<ObjectiveType, ObjectiveIndicator>();

    public void GetObjective(ObjectiveType type)
    {
        if (remaining.ContainsKey(type))
        {
            OnObjectiveCollected.Invoke();
            remaining[type]--;
            indicators[type].SetRemaining(remaining[type]);
            if (remaining[type] == 0)
            {
                remaining.Remove(type);
            }
            if (remaining.Count == 0)
            {
                OnObjectivesCompleted();
            }
        }
    }

    private void Start() 
    {
        var data = FindObjectOfType<ContractData>();
        if (data)
        {
            objectives = data.terms.objectives;
        }
        foreach (var obj in objectives)
        {
            remaining.Add(obj.type, obj.quantity);
            var indicator = GameObject.Instantiate(objectiveIndicatorPrefab, objectivesHUD).GetComponent<ObjectiveIndicator>();
            indicator.icon.sprite = GetObjectiveSprite(obj.type);
            indicator.SetRemaining(obj.quantity);
            indicators[obj.type] = indicator;
        }
    }

    void OnObjectivesCompleted()
    {
        print("Objectives complete!");
        objectivesComplete = true;
        OnAllObjectivesCollected.Invoke();
    }

    Sprite GetObjectiveSprite(ObjectiveType type)
    {
        foreach (var data in objectiveData)
        {
            if (data.type == type) return data.icon;
        }
        return null;
    }
}
