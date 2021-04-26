using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lantern : MonoBehaviour
{
    public TextMeshProUGUI countdown;
    public float life;

    public SinFlicker flicker;

    private void Start() 
    {
        life = 50.0f + FindObjectOfType<ContractData>().terms.depth * 2;
    }
    void Update()
    {
        if (life < 10.0f)
        {
            flicker.intensityMultiplier = life/10.0f;
            flicker.flickerFreq = 2 + (10 - life);
        } else
        {
            flicker.intensityMultiplier = 1.0f;
        }
        life -= Time.deltaTime;
        life = Mathf.Max(0.0f, life);
        countdown.text = ((int)life).ToString();
        if (life == 0.0f)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.1792453f, 0.1792453f, 0.1792453f);// Dead lantern
        }
    }
}
