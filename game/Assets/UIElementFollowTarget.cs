using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElementFollowTarget : MonoBehaviour
{
    public GameObject target;

    void LateUpdate()
    {
        if (target)
        {
            transform.position = target.transform.position;        
        }
    }
}
