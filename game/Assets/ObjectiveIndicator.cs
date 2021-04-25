using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectiveIndicator : MonoBehaviour
{
    public Image icon;
    public int quantity = 1;

    public TextMeshProUGUI text;
    public Image completed;

    public void SetRemaining(int num)
    {
        if (num == 0)
        {
            text.gameObject.SetActive(false);
            completed.gameObject.SetActive(true);
        } else
        {
            text.text = "x" + num.ToString();
        }
    }
}
