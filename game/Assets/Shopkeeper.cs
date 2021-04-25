using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public enum PlayerSkill
{
    MoveRate,
    MineSpeed,
    Lantern,
    Negotiation
};

[System.Serializable]
public struct UpgradeData
{
    public PlayerSkill skill;
    public int cost;
};

[System.Serializable]
public struct UpgradeSkillSnippet
{
    public PlayerSkill skill;
    public List<string> quips;
}

public class Shopkeeper : MonoBehaviour
{
    public bool spawnedLower = true;
    public UnityEvent OnSkillPurchased;
    public List<UpgradeSkillSnippet> snippets;
    public UpgradeData offer;
    private Shop shop;

    private ContractData contract;

    private Mouth mouth;
    private void Start() 
    {
        contract = FindObjectOfType<ContractData>();
        mouth = FindObjectOfType<Mouth>();
        shop = FindObjectOfType<Shop>();
        shop.AttemptPurchase.AddListener(OnAttemptPurchase);
        offer = GetUpgrade();
        shop.purchaseCost.text = "$" + offer.cost;
        List<string> quips = snippets.Find(x => x.skill == offer.skill).quips;
        shop.purchaseLabel.text = quips[Mathf.Min(quips.Count - 1, contract.levels[offer.skill])];
        shop.gameObject.SetActive(false); // must be active to be found :(
    }

    UpgradeData GetUpgrade()
    {
        UpgradeData upgrade = new UpgradeData();
        upgrade.skill = (PlayerSkill)Random.Range(0, 3);
        upgrade.cost = (int)(contract.levels[upgrade.skill] * 100 * Random.Range(0.5f, 2.0f));
        return upgrade;
    }

    private void Update() {
        var rt = shop.purchasePanel.GetComponent<RectTransform>();
        if (mouth.teethSwapped)
        {
            rt.localScale = new Vector3(1.0f, spawnedLower ? -1.0f : 1.0f, 1.0f);
        } 
        else
        {
            rt.localScale = new Vector3(1.0f, spawnedLower ? 1.0f : -1.0f, 1.0f);
        }
        shop.bankLabel.text = "$" + contract.bank;
        if (contract.bank >= offer.cost)
        {
            shop.purchaseCost.color = new Color(87/255.0f, 144/255.0f, 32/255.0f);
        } 
        else
        {
            shop.purchaseCost.color = Color.red;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        shop.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        shop.gameObject.SetActive(false);    
    }

    public void OnAttemptPurchase()
    {
        print("Attempt purchase");
        var data = FindObjectOfType<ContractData>();
        if (data.bank >= offer.cost)
        {
            data.bank -= offer.cost;
            data.levels[offer.skill]++;
            OnSkillPurchased.Invoke();
            shop.gameObject.SetActive(false);
            Destroy(transform.parent.gameObject);
        }
    }
}
