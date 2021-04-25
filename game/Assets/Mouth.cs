using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
{
    public GameObject upper;
    public GameObject lower;

    public bool teethSwapped = false;
    public Smasher smasher;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Swaps upper and lower teeth
    public void SwapTeeth()
    {
        if (smasher.mouthState == Smasher.MouthState.Closed)
        {
            GameObject top = null;
            GameObject bottom = null;
            if (teethSwapped)
            {
                top = upper;
                bottom = lower;
            } 
            else
            {
                top = lower;
                bottom = upper;
            }
            top.transform.localScale = new Vector3(1.0f, -1.0f, 1.0f);
            top.layer = LayerMask.NameToLayer("TopTeeth");
            float tempy = top.transform.localPosition.y;
            top.transform.position = new Vector3(top.transform.position.x, bottom.transform.position.y, 0.0f);
            bottom.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            bottom.layer = LayerMask.NameToLayer("BottomTeeth");
            teethSwapped = !teethSwapped;
            bottom.transform.position = new Vector3(bottom.transform.position.x, tempy, 0.0f);
            smasher.crush = top.transform;
        }
    }
}
