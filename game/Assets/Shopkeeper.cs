using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shopkeeper : MonoBehaviour
{
    public GameObject shop;

    public GameObject purchasePanel;

    public Mouth mouth;

    private void Update() {
        var rt = purchasePanel.GetComponent<RectTransform>();
        rt.localScale = new Vector3(1.0f, mouth.teethSwapped ? 1.0f : -1.0f, 1.0f);
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        shop.SetActive(true);    
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        shop.SetActive(false);    
    }
}
