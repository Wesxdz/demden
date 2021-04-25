using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class Shop : MonoBehaviour
{
    public UnityEvent AttemptPurchase;
    public GameObject purchasePanel;
    public TextMeshProUGUI purchaseLabel;
    public TextMeshProUGUI purchaseCost;
    public TextMeshProUGUI bankLabel;

    public void BroadcastAttemptPurchase()
    {
        AttemptPurchase.Invoke();
    }
}
