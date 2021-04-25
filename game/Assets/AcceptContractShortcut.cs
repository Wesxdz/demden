using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AcceptContractShortcut : MonoBehaviour
{
    public UnityEvent OnPressEnter;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnPressEnter.Invoke();
        }
        
    }
}
