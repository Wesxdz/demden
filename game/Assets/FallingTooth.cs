using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTooth : MonoBehaviour
{
    public float triggerFallRange = 1.0f;
    private PlayerMovement player;
    private Smasher smasher;

    private bool isFalling = false;

    private void Start() 
    {
        player = FindObjectOfType<PlayerMovement>();
        smasher = FindObjectOfType<Smasher>();
    }

    private void Update() 
    {
        if (isFalling)
        {

        }
        else
        {
            if (smasher.mouthState == Smasher.MouthState.Open && Mathf.Abs(player.transform.position.x - transform.position.x) < triggerFallRange)
            {
                BreakLoose();
            }
        }
    }

    private void BreakLoose()
    {
        transform.parent = transform.parent.parent;
        isFalling = true;
        GetComponent<Rigidbody2D>().simulated = true;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("BottomTeeth"))
        {
            Destroy(gameObject);
        }
    }
}
