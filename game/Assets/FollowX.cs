using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowX : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update

    public float minX;
    void Start()
    {
    }

    // Update is called once per frame
    private void LateUpdate() 
    {
        if (target)
        {
            transform.position = new Vector3(Mathf.Max(minX, target.position.x), transform.position.y, transform.position.z);
        }
    }
}
